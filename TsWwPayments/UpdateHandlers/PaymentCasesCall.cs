using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TsWwPayments.Repositories;
using ZarinSharp;

namespace TsWwPayments.UpdateHandlers
{
    public class PaymentCasesCall : SimpleDiHandler<CallbackQuery>
    {
        private readonly IZarinClient _zarinClient;
        private readonly PaymentsAccountRepository accounts;
        private readonly TransmissionRepository transmissions;

        public PaymentCasesCall(
            IZarinClient zarinClient,
            PaymentsAccountRepository paymentsAccounts,
            TransmissionRepository transmissions)
        {
            _zarinClient = zarinClient;
            this.accounts = paymentsAccounts;
            this.transmissions = transmissions;
        }

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

            .ElseIf("^pay_ensure_", async x =>
            {
                var caseName = x.Update.Data![11..];

                var item = PaymentCaseData.GetInfo(caseName);

                if (item == null)
                {
                    await x.Edit(text: "Try again!");
                    return;
                }

                await x.Edit(text: $"آیا از تصمیم خود مطمعن هستید ؟\n\nنام ایتم: {item.Value.descriptor.Name}\n{item.Value.descriptor.Description}",
                    inlineKeyboardMarkup: new InlineKeyboardMarkup(
                        new InlineKeyboardButton[]
                        {
                            InlineKeyboardButton.WithCallbackData("بله", $"pay_requestPay_{caseName}"),
                            InlineKeyboardButton.WithCallbackData("خیر", "pay_cancel"),
                        }));
                await x.Answer();
            })

            .ElseIf("^pay_requestPay_", async context =>
            {
                var caseName = context.Update.Data![15..];

                var item = PaymentCaseData.GetPaymentItem(caseName);
                if (item == null)
                {
                    await context.Edit(text: "Try again!");
                    return;
                }

                try
                {

                    var acc = await accounts.FindOneAsync(x=> x.TelegramId == context.SenderId());
                    if (acc == null)
                    {
                        acc = await accounts.CreateAccount(context.SenderId(), context.Sender().FirstName);

                        await context.Send($"#Notify\nCreated payment account.\nID: {acc.PaymentsAccountId}");
                    }

                    var payReq = await _zarinClient.PaymentRequestAsync(
                        item.Amount,
                        item.Description + $"\n{context.Update.From.FirstName} ( {context.Update.From.Id} )",
                        currency: item.Currency);

                    await transmissions.InitTransmission(
                        acc.PaymentsAccountId,
                        payReq.Authority,
                        caseName,
                        item.Amount,
                        item.Currency);

                    await context.Edit(text: "از لینک زیر برای پرداخت استفاده کنید.",
                        inlineKeyboardMarkup: new InlineKeyboardMarkup(
                            InlineKeyboardButton.WithUrl("پرداخت", payReq.GetStartPaymentUrl())));
                }
                catch(ZarinpalException e)
                {
                    await context.Edit(text: $"Pay request error: {e.Code} - {e.Description}");
                    await context.Answer();
                }
            })

            .ElseIf("^pay_cancel$", async x =>
            {
                await x.Edit(text: "Cancelled!");
                await x.Answer();
            })

            .Else(async x => await x.Answer($"Undefined! {x.Update.Data}"));
        }
    }
}
