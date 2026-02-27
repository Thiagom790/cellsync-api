using CellSync.Domain.Enums;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using Microsoft.Extensions.Logging;

namespace CellSync.Application.Events;

[EventMessageHandle(EventNames.REGISTER_VISITOR)]
public class RegisterVisitorMessageHandler(ILogger<RegisterVisitorMessageHandler> logger) : IEventMessageHandler<RegisterVisitorEventMessage>
{
    public Task OnReceiveEventAsync(RegisterVisitorEventMessage eventData)
    {
        Console.WriteLine($"Registered visitor handler {eventData.Name}");
        logger.LogInformation("Registered visitor handler {name}", eventData.Name);

        return Task.CompletedTask;
    }
}