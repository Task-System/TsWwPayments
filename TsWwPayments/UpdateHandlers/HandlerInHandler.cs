using SimpleUpdateHandler;
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

            await _processor.CarryUserResponse(ctx.Update.From!.Id, privateOnly: true)
                .IfNotNull(async x =>
                {
                    await x.If("^hello ", async y => await y.Response("Hello There!"))
                        .Else(async x => await x.Response("Undefined response."));
                })
                .Else(async x => await x.Response("You're timed out."));
        }
    }
}
