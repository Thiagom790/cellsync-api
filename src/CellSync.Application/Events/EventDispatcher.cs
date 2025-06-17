using System.Reflection;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Application.Events;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly Dictionary<string, Type> _messageTypes = GetMessageTypes();

    private static Dictionary<string, Type> GetMessageTypes()
    {
        var handlerInterface = typeof(IEventMessageHandler<>);

        var handlerClasses = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type =>
                type is { IsClass: true, IsAbstract: false } &&
                type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
            )
            .ToList();

        var messageTypes = handlerClasses
            .Select(type => new
            {
                EventName = type.GetCustomAttribute<EventMessageHandleAttribute>()?.EventName,
                MessageType = type.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerInterface)
                    .GetGenericArguments()
                    .FirstOrDefault()
            })
            .Where(x => x.EventName is not null && x.MessageType is not null)
            .ToDictionary(x => x.EventName!, x => x.MessageType!);

        return messageTypes;
    }

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