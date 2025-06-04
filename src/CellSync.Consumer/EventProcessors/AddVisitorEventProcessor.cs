using CellSync.Domain.Entities;
using CellSync.Domain.Enums;

namespace CellSync.Consumer.EventProcessors;

[EventProcessor(EventNames.ADD_VISITOR)]
public class AddVisitorEventProcessor : IEventProcessor<Member>
{
    public Task OnReceiveEventAsync(Member eventData) // TODA CLASSE PROCESSADORA DEVE TER ESSE METODO DEFINIDO NA INTERFACE IEventProcessor<T>
    {
        Console.WriteLine(eventData?.Name);

        return Task.CompletedTask;
    }
}

