using Microsoft.AspNetCore.Mvc;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.Controllers;

public class WebhookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post(
        [FromServices]SimpleDiUpdateProcessor updateProcessor,
        [FromBody]Update update)
    {
        await updateProcessor.ProcessSimpleHandlerAsync(update);
        return Ok();
    }
}
