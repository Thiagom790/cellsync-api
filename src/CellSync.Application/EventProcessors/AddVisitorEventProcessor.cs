using CellSync.Domain.Entities;
using CellSync.Domain.Events;

namespace CellSync.Application.EventProcessors;

public class AddVisitorEventProcessor : IEventProcessor<Member>
{
    public Task OnReceiveEventAsync(Member eventData)
    {
        Console.WriteLine("Add Visitor");
        
        return Task.CompletedTask;
    }
}