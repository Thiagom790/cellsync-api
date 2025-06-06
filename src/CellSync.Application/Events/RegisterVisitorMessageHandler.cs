using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;

namespace CellSync.Application.Events;

public class RegisterVisitorMessageHandler : IEventMessageHandler
{
    public Task HandleAsync(IEventMessage eventData)
    {
        var message = (RegisterVisitorEventMessage)eventData;

        Console.WriteLine($"Registered visitor {message.Name}");

        return Task.CompletedTask;
    }

    public static Type MessageType { get; } = typeof(RegisterVisitorEventMessage);
}