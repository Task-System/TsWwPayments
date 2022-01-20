using Telegram.Bot.Types.ReplyMarkups;
using TsWwPayments.Models;
using ZarinSharp.Types.Enums;

namespace TsWwPayments
{
    public static class PaymentCaseData
    {
        public static string AsPersian(this Currency currency)
            => currency == Currency.IRR ? "ریال" : "تومان";

        public static string KeyButtonText(this PaymentCase paymentCase)
        {
            if (paymentCase.CaseType == PaymentCaseType.SealedCase)
                return $"{paymentCase.Name} به مبلغ {paymentCase.Price} {paymentCase.Currency.AsPersian()}";
            else if (paymentCase.CaseType == PaymentCaseType.AbstractCase)
                return $"{paymentCase.Name}";
            else
                return $"{paymentCase.Name} ( مبلغ بین {paymentCase.PriceRange!.Value.Minimum} تا {paymentCase.PriceRange!.Value.Maximum} {paymentCase.Currency.AsPersian()} )";
        }

        public static (string, string) KeyButtonTextData(this PaymentCase paymentCase)
        {
            if (paymentCase.CaseType == PaymentCaseType.SealedCase)
                return ($"{paymentCase.Name} به مبلغ {paymentCase.PriceRange} {paymentCase.Currency.AsPersian()}",
                    $"pay_ensure_{paymentCase.Id}");
            else if (paymentCase.CaseType == PaymentCaseType.AbstractCase)
                return ($"{paymentCase.Name}", $"pay_showItems_{paymentCase.Id}");
            else
                return ($"{paymentCase.Name} ( مبلغ بین {paymentCase.PriceRange!.Value.Minimum} تا {paymentCase.PriceRange!.Value.Maximum} {paymentCase.Currency.AsPersian()} )", 
                    $"pay_askPrice_{paymentCase.Id}");
        }

        public static string KeyButtonData(this PaymentCase paymentCase)
        {
            if (paymentCase.CaseType == PaymentCaseType.SealedCase)
                return $"pay_ensure_{paymentCase.Id}";
            else if (paymentCase.CaseType == PaymentCaseType.AbstractCase)
                return $"pay_showItems_{paymentCase.Id}";
            else
                return $"pay_askPrice_{paymentCase.Id}";
        }

        public static string KeyButtonData(this PaymentItem paymentItem)
            => $"pay_ensure_{paymentItem.Id}";

        public static string KeyButtonText(this PaymentItem paymentItem)
            => $"{paymentItem.Name} به مبلغ {paymentItem.Amount} {paymentItem.Currency.AsPersian()}";

        public static PaymentCase? GetPaymentCase(string caseId)
            => PaymentCases.FirstOrDefault(x=> x.Id == caseId);

        public static PaymentItem? GetPaymentItem(this PaymentCase paymentCase, string id)
            => paymentCase.Items?.FirstOrDefault(x => x.Id == id);

        public static PaymentItem? GetPaymentItem(string id)
            => PaymentCases
                .Where(x=> x.Items is not null)
                .SelectMany(x=> x.Items!).FirstOrDefault(x => x.Id == id);

        public static InlineKeyboardMarkup Init()
        {
            return new InlineKeyboardMarkup(PaymentCases.Select(x =>
            {
                var info = x.KeyButtonTextData();
                return new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData(info.Item1, info.Item2)
                };
            }));
        }

        public static (IPaymentDescriptor descriptor, InlineKeyboardMarkup markup)? GetInfo(
            string id)
        {
            var parts = id.Split('_');
            if (parts.Length < 2)
            {
                // It's a payment case
                var paymentCase = GetPaymentCase(id);
                if (paymentCase == null)
                    return null;

                var keyInfo = paymentCase.KeyButtonTextData();

                return (paymentCase, new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithCallbackData(
                        keyInfo.Item1, keyInfo.Item2)));
            }
            else
            {
                // It's a payment item
                var paymentCase = GetPaymentCase(parts[0]);
                if (paymentCase == null)
                    return null;

                var item = paymentCase.GetPaymentItem(id);
                if (item == null)
                    throw new Exception($"No item for {id}");

                return (item, new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithCallbackData(
                        item.KeyButtonText(), item.KeyButtonData())));
            }
        }

        public static InlineKeyboardMarkup GetItems(this PaymentCase paymentCase)
            => new(paymentCase.Items!.Select(x =>
                {
                    var text = x.KeyButtonText();
                    var data = x.KeyButtonData();
                    return new InlineKeyboardButton[]
                    {
                        InlineKeyboardButton.WithCallbackData(text, data)
                    };
                }));

        public static readonly IReadOnlyCollection<PaymentCase> PaymentCases 
            = new HashSet<PaymentCase>
            {
                new PaymentCase(
                    "donate", "حمایت مالی",
                    "در این بخش می توانید در جهت حمایت مالی از ربات پرداختی را داشته باشید.",
                    priceRange: (2000, 500000)),

                new PaymentCase(
                    "taskyCoinBundle", "خرید TaskyCoin 🀄!",
                    "در این بخش می توانید TaskyCoin🀄 بخرید که برای خرید ایتم های درون @TsWwPlus_Bot مورد استفاده قرار می گیرد.",
                    items: new HashSet<PaymentItem>
                    {
                        // 1 TaskyCoin = 1,000 Toman * 1,000 TaskyCoin (TC) = 1,000,000 Toman!
                        // 10  TC = 10K  IRT + fee (1500 IRT) = 11.5K  IRT
                        // 20  TC = 20K  IRT + fee (1500 IRT) = 21.5K  IRT
                        // 50  TC = 50K  IRT + fee (1500 IRT) = 51.5K  IRT
                        // 75  TC = 75k  IRT + fee (1500 IRT) = 76.5K  IRT
                        // 100 TC = 100K IRT + fee (1500 IRT) = 101.5K IRT

                        new PaymentItem("taskyCoinBundle_1", "10 🀄", 11500, "خرید 10 TaskyCoin🀄",    Currency.IRT, typeof(TaskyCoinStatusAction)),
                        new PaymentItem("taskyCoinBundle_2", "20 🀄", 21500, "خرید 20 TaskyCoin🀄",    Currency.IRT, typeof(TaskyCoinStatusAction)),
                        new PaymentItem("taskyCoinBundle_3", "50 🀄", 51500, "خرید 50 TaskyCoin🀄",    Currency.IRT, typeof(TaskyCoinStatusAction)),
                        new PaymentItem("taskyCoinBundle_4", "75 🀄", 76500, "خرید 75 TaskyCoin🀄",    Currency.IRT, typeof(TaskyCoinStatusAction)),
                        new PaymentItem("taskyCoinBundle_5", "100 🀄", 101500, "خرید 100 TaskyCoin🀄", Currency.IRT, typeof(TaskyCoinStatusAction)),
                    })
            };
    }
}
