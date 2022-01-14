using Microsoft.AspNetCore.Mvc;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.Controllers;

public class WebhookController : ControllerBase
{
    [HttpPost]
    public IActionResult Post(
        [FromServices] SimpleDiUpdateProcessor updateProcessor,
        [FromBody] Update update)
    {
        updateProcessor.Handle(update);
        return Ok();
    }
}
