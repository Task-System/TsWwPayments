using SimpleUpdateHandler;
using SimpleUpdateHandler.DependencyInjection;
using Telegram.Bot.Types;

namespace TsWwPayments.UpdateHandlers
{
    public class HandleStartCommand : SimpleDiHandler<Message>
    {
        protected override async Task HandleUpdate(SimpleContext<Message> ctx)
        {
            await ctx.If(x => x.IsPrivate(), x => x.Response("Started!"));
        }
    }
}
