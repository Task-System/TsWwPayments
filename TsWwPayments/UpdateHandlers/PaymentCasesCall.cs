using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.UpdateHandlers
{
    public class PaymentCasesCall : SimpleDiHandler<CallbackQuery>
    {
        protected override async Task HandleUpdate(SimpleContext<CallbackQuery> ctx)
        {
            await ctx.If("^pay_showItems_", async x =>
            {
                var caseName    = x.Update.Data![14..];
                var paymentCase = PaymentCaseData.GetPaymentCase(caseName);

                await x.Edit(text: paymentCase!.Description!,
                    inlineKeyboardMarkup: paymentCase!.GetItems());
                await x.Answer(caseName, true);
            })

            .ElseIf("^pay_askPrice_", async x=>
            {
                await x.Answer();
            })

            .Else(async x => await x.Answer("Undefined!"));
        }
    }
}
