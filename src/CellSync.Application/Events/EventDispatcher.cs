using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Application.Events;

public class EventDispatcher(IServiceProvider serviceProvider, IEventMessageHandlerMapProvider mapProvider)
    : IEventDispatcher
{
    private readonly Dictionary<string, Type> _messageTypes = mapProvider.GetMessageTypes();

    public async Task DispatchAsync(string eventType, IEventMessage message)
    {
        if (!_messageTypes.TryGetValue(eventType, out var messageType))
        {
            Console.WriteLine($"No handler registered for event type {eventType}");
            return;
        }

        using var scope = serviceProvider.CreateScope();

        var interfaceType = typeof(IEventMessageHandler<>).MakeGenericType(messageType);
        dynamic? handlerInstance = scope.ServiceProvider.GetService(interfaceType);

        if (handlerInstance is null)
        {
            Console.WriteLine($"No handler instance found for event type {eventType}");
            return;
        }

        await handlerInstance.OnReceiveEventAsync((dynamic)message);
    }

    public Type? GetMessageType(string? eventType) => string.IsNullOrWhiteSpace(eventType)
        ? null
        : _messageTypes.GetValueOrDefault(eventType);
}