using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TsWwPayments.UpdateHandlers
{
    public class HandlePayCommand : SimpleDiHandler<Message>
    {
        protected override async Task HandleUpdate(SimpleContext<Message> ctx)
        {
            await ctx.Response("This is my keyboard!",
                replyMarkup: new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithCallbackData("Don't click here!", "pay_cases_test")));
        }
    }
}
