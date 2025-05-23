using CellSync.Domain.Entities;

namespace CellSync.Consumer.EventProcessors;

public class AddVisitorEventProcessor : IEventProcessor<Member>
{
    public Task OnReceiveEventAsync(Member eventData)
    {
        Console.WriteLine(eventData?.Name);

        return Task.CompletedTask;
    }
}