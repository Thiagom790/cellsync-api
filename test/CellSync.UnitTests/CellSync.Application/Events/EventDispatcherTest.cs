using CellSync.Application.Events;
using CellSync.Domain.Events.Config;
using CellSync.Domain.Events.Messages;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace CellSync.UnitTests.CellSync.Application.Events;

public class FakeEventMessage : IEventMessage
{
    public string Content { get; set; } = string.Empty;
}

public class EventDispatcherTest
{
    private const string FakeEventName = "FakeEvent";

    [Fact]
    public async Task DispatchAsync_ValidEventType_ShouldCallHandler()
    {
        var fakeHandler = Substitute.For<IEventMessageHandler<FakeEventMessage>>();

        var scopedProvider = Substitute.For<IServiceProvider>();
        scopedProvider.GetService(typeof(IEventMessageHandler<FakeEventMessage>)).Returns(fakeHandler);

        var fakeScope = Substitute.For<IServiceScope>();
        fakeScope.ServiceProvider.Returns(scopedProvider);

        var scopeFactory = Substitute.For<IServiceScopeFactory>();
        scopeFactory.CreateScope().Returns(fakeScope);

        var rootProvider = Substitute.For<IServiceProvider>();
        rootProvider.GetService(typeof(IServiceScopeFactory)).Returns(scopeFactory);

        var mapProvider = Substitute.For<IEventMessageHandlerMapProvider>();
        mapProvider.GetMessageTypes().Returns(new Dictionary<string, Type>
        {
            { FakeEventName, typeof(FakeEventMessage) }
        });

        var dispatcher = new EventDispatcher(rootProvider, mapProvider);

        var message = new FakeEventMessage { Content = "Test message" };

        await dispatcher.DispatchAsync(FakeEventName, message);

        await fakeHandler.Received(1).OnReceiveEventAsync(message);
    }

    [Fact]
    public void GetMessageType_NullOrEmptyEventType_ShouldReturnNull()
    {
        var mapProvider = Substitute.For<IEventMessageHandlerMapProvider>();
        var dispatcher = new EventDispatcher(Substitute.For<IServiceProvider>(), mapProvider);

        var result = dispatcher.GetMessageType(null);
        Assert.Null(result);

        result = dispatcher.GetMessageType(string.Empty);
        Assert.Null(result);
    }

    [Fact]
    public void GetMessageType_ValidEventType_ShouldReturnMessageType()
    {
        var expectedType = typeof(FakeEventMessage);
        var mapProvider = Substitute.For<IEventMessageHandlerMapProvider>();
        mapProvider.GetMessageTypes().Returns(new Dictionary<string, Type>
        {
            { FakeEventName, expectedType }
        });

        var dispatcher = new EventDispatcher(Substitute.For<IServiceProvider>(), mapProvider);

        var result = dispatcher.GetMessageType(FakeEventName);
        Assert.Equal(expectedType, result);
    }
}