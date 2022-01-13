using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.UpdateHandlers
{
    public class PaymentCasesCall : SimpleDiHandler<CallbackQuery>
    {
        protected override async Task HandleUpdate(SimpleContext<CallbackQuery> ctx)
        {
            await ctx.Answer("Tested!", true);
        }
    }
}
