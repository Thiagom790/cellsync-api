namespace CellSync.Domain.Events;

public interface IEventProcessor<in TEvent>
{
    Task OnReceiveEventAsync(TEvent eventData);
}