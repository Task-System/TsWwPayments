using SimpleUpdateHandler;
using SimpleUpdateHandler.CustomFilters;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.UpdateHandlers
{
    public class HandlerInHandler : SimpleDiHandler<Message>
    {
        private readonly SimpleDiUpdateProcessor _processor;

        public HandlerInHandler(SimpleDiUpdateProcessor processor)
        {
            _processor = processor;
        }

        protected override async Task HandleUpdate(SimpleContext<Message> ctx)
        {
            await ctx.Response("Say hello ...");

            var m = await _processor.RegisterCarryHandler(
                $"{ctx.Update.From!.Id}HnH",
                FilterCutify.PM()
                    .And(FilterCutify.MsgOfUsers(ctx.Update.From.Id))
                        .And(FilterCutify.TextMatchs("^hello")));

            if (m == null)
            {
                await ctx.Response("Timed out");
                return;
            }

            await m.Response("Hello there!", true);
        }
    }
}
