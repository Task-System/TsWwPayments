using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using TsWwPayments.Services;

namespace TsWwPayments.Controllers;

public class WebhookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
    {
        await handleUpdateService.HandleTheShit(update);
        return Ok();
    }
}
