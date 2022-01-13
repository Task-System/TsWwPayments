using ImRepositoryPattern;
using Microsoft.EntityFrameworkCore;
using SimpleUpdateHandler.CustomFilters;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot;
using TsWwPayments;
using TsWwPayments.Databases;
using TsWwPayments.Repositories;
using TsWwPayments.Services;
using TsWwPayments.UpdateHandlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var botConfigs = builder.Configuration.GetSection("BotConfigs").Get<BotConfigs>();

// There are several strategies for completing asynchronous tasks during startup.
// Some of them could be found in this article https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/
// We are going to use IHostedService to add and later remove Webhook
builder.Services.AddHostedService<ConfigureWebhook>();
// builder.Services.AddHostedService<DatabasePreload>();

// Register named HttpClient to get benefits of IHttpClientFactory
// and consume it with ITelegramBotClient typed client.
// More read:
//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
//  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
builder.Services.AddHttpClient("tgwebhook")
    .AddTypedClient<ITelegramBotClient>(httpClient
        => new TelegramBotClient(botConfigs.BotToken, httpClient));

// Telegram updates helper stuff
builder.Services.AddUpdateProcessor(configure => configure
    .RegisterMessage<HandleStartCommand>(FilterCutify.OnCommand("start"))
    .RegisterMessage<HandlePayCommand>(FilterCutify.OnCommand("pay"))
    .RegisterCallbackQuery<PaymentCasesCall>(FilterCutify.DataMatches("^pay_cases")));

// Database helper stuff
builder.Services.AddScoped<IUnitOfWork<PaymentsContext>>(
    x=> new ScopedUnitOfWork<PaymentsContext>(
        x.GetRequiredService<PaymentsContext>(),
        typeof(TransmissionRepository)));

// The Telegram.Bot library heavily depends on Newtonsoft.Json library to deserialize
// incoming webhook updates and send serialized responses back.
// Read more about adding Newtonsoft.Json to ASP.NET Core pipeline:
//   https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-5.0#add-newtonsoftjson-based-json-format-support
builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext<PaymentsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PaymentsConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Configure custom endpoint per Telegram API recommendations:
    // https://core.telegram.org/bots/api#setwebhook
    // If you'd like to make sure that the Webhook request comes from Telegram, we recommend
    // using a secret path in the URL, e.g. https://www.example.com/<token>.
    // Since nobody else knows your bot's token, you can be pretty sure it's us.
    var token = botConfigs.BotToken;
    endpoints.MapControllerRoute(name: "tgwebhook",
                                 pattern: $"bot/{token}",
                                 new { controller = "Webhook", action = "Post" });
    endpoints.MapControllers();
});

app.Run();
