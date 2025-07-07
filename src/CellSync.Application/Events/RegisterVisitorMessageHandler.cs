using CellSync.Domain.Enums;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;

namespace CellSync.Application.Events;

[EventMessageHandle(EventNames.REGISTER_VISITOR)]
public class RegisterVisitorMessageHandler : IEventMessageHandler<RegisterVisitorEventMessage>
{
    public Task OnReceiveEventAsync(RegisterVisitorEventMessage eventData)
    {
        Console.WriteLine($"Registered visitor handler {eventData.Name}");

        return Task.CompletedTask;
    }
}