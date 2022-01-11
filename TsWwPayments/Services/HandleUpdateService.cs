using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TsWwPayments.Models;

namespace TsWwPayments.Services;

public class HandleUpdateService
{
    private readonly ITelegramBotClient _botClient;
    private readonly ILogger<HandleUpdateService> _logger;
    private readonly SimpleDiUpdateProcessor _updateProcessor;

    public HandleUpdateService(
        ITelegramBotClient botClient,
        ILogger<HandleUpdateService> logger,
        SimpleDiUpdateProcessor updateProcessor)
    {
        _botClient = botClient;
        _logger = logger;
        _updateProcessor = updateProcessor;
    }

    public async Task HandleTheShit(Update update)
    {
        await _updateProcessor.ProcessSimpleHandlerAsync(update);
    }

    private async Task BotOnMessageReceived(Message message)
    {
        switch (message)
        {
            case { Text: { } text } when text.StartsWith("/start"):
                {
                    await _botClient.SendTextMessageAsync(message.Chat.Id, "Started! To Check ... ");
                    return;
                }

            case { Text: { } text } when text.StartsWith("/pay"):
                {
                    await _botClient.SendTextMessageAsync(
                        message.Chat.Id,
                        "Let's see payment informations",
                        replyMarkup: PaymentCaseData.Init());
                    return;
                }
        }
    }

    // Process Inline Keyboard callback data
    private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
    {
        switch (callbackQuery)
        {
            case { Data: { } data, Message: { } message }:
                {
                    var parts = data.Split('_');
                    if (parts[0].StartsWith("pay"))
                    {
                        var director = parts[1];
                        var id = parts[2];

                        var info = PaymentCaseData.GetInfo(id);

                        if (director == "showItems")
                        {
                            var items = ((PaymentCase)info.Value.descriptor).GetItems();

                            await _botClient.EditMessageTextAsync(
                                message.Chat.Id,
                                message.MessageId,
                                info.Value.descriptor.Description,
                                replyMarkup: items);
                        }
                        else if (director == "askPrice")
                        {
                            await _botClient.EditMessageTextAsync(
                                message.Chat.Id,
                                message.MessageId,
                                info.Value.descriptor.Description);

                            await _botClient.SendTextMessageAsync(
                                message.Chat.Id,
                                "Enter a value");
                        }
                        else
                        {
                            await _botClient.EditMessageTextAsync(
                                message.Chat.Id,
                                message.MessageId,
                                info.Value.descriptor.Description,
                                replyMarkup: info.Value.markup);
                        }
                    }

                    return;
                }
        }
    }

    private Task UnknownUpdateHandlerAsync(Update update)
    {
        _logger.LogInformation("Unknown update type: {updateType}", update.Type);
        return Task.CompletedTask;
    }

    public Task HandleErrorAsync(Exception exception)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        _logger.LogInformation("HandleError: {ErrorMessage}", ErrorMessage);
        return Task.CompletedTask;
    }
}
