using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.UpdateHandlers
{
    public class PaymentCasesCall : SimpleDiHandler<CallbackQuery>
    {
        protected override async Task HandleUpdate(SimpleContext<CallbackQuery> ctx)
        {
            await ctx.If("pay_cases_part1", x => x.Answer("On part 1"))
                .ElseIf("pay_case_part2", x => x.Answer("On part 2"))
                .Else(x => x.Answer("On part Unknown"));
        }
    }
}
