using CellSync.Domain.Events.Messages;

namespace CellSync.Domain.Events.Config;

public interface IEventMessageHandler<in TEventMessage> where TEventMessage : IEventMessage
{
    public Task OnReceiveEventAsync(TEventMessage eventData);
}