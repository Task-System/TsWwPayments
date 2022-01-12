using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.UpdateHandlers
{
    public class PaymentCasesCall1 : SimpleDiHandler<CallbackQuery>
    {
        protected override Task HandleUpdate(SimpleContext<CallbackQuery> ctx)
        {
            throw new NotImplementedException();
        }
    }
}
