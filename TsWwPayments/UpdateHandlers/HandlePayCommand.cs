using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.UpdateHandlers
{
    public class HandlePayCommand : SimpleDiHandler<Message>
    {
        protected override async Task HandleUpdate(SimpleContext<Message> ctx)
        {
            await ctx.Response("Here are the payment cases available!",
                replyMarkup: PaymentCaseData.Init());
        }
    }
}
