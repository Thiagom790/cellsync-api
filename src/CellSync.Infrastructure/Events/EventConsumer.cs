using CellSync.Domain.Events.Config;
using Microsoft.Extensions.Hosting;

namespace CellSync.Infrastructure.Events;

public class EventConsumer(InMemoryMessageQueue queue, IEventDispatcher dispatcher) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in queue.DequeueAllAsync(stoppingToken))
        {
            Console.WriteLine("EventConsumer received message: Id={0}, Type={1}, Time={2:yyyy-MM-dd HH:mm:ss}",
                message.Id, message.Type, DateTime.Now);

            await dispatcher.DispatchAsync(message.Type, message);
        }
    }
}