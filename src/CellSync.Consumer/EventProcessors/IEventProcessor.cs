using Azure.Messaging.EventHubs;

namespace CellSync.Consumer.EventProcessors;

public interface IEventProcessor<in TEvent>
{
    Task OnReceiveEventAsync(TEvent eventData);
}