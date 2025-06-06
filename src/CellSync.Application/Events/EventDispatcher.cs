using System.Reflection;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Application.Events;

public class EventDispatcher(IServiceProvider serviceProvider) : IEventDispatcher
{
    private readonly Dictionary<string, Type> _handlers = RegisterHandlers();
    private readonly Dictionary<string, Type> _messageTypes = RegisterMessageTypes();

    private static Dictionary<string, Type> RegisterHandlers()
    {
        var handlerTypes = typeof(IEventMessageHandler);

        return Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => handlerTypes.IsAssignableFrom(type) && type is { IsClass: true, IsAbstract: false })
            .ToDictionary(
                type => ((Type)type.GetProperty(nameof(IEventMessageHandler.MessageType))!.GetValue(null)!).Name,
                type => type
            );
    }

    private static Dictionary<string, Type> RegisterMessageTypes()
    {
        var messageTypes = typeof(IEventMessage);

        return messageTypes.Assembly.DefinedTypes
            .Where(type => messageTypes.IsAssignableFrom(type) && type is { IsAbstract: false, IsInterface: false })
            .ToDictionary(
                type => (string)type.GetProperty(nameof(IEventMessage.MessageType))!.GetValue(
                    Activator.CreateInstance(type))!,
                type => type.AsType()
            );
    }

    public async Task DispatchAsync(IEventMessage message)
    {
        if (_handlers.TryGetValue(message.GetType().Name, out var handlerType))
        {
            using var scope = serviceProvider.CreateScope();
            var handlerInstance = (IEventMessageHandler)scope.ServiceProvider.GetRequiredService(handlerType);
            await handlerInstance.HandleAsync(message);
        }
        else
        {
            Console.WriteLine($"No handler for type {message.GetType().Name}");
        }
    }

    public Type? GetMessageType(string? messageTypeName) => string.IsNullOrWhiteSpace(messageTypeName)
        ? null
        : _messageTypes.GetValueOrDefault(messageTypeName);
}