using System.Threading.Channels;
using CellSync.Domain.Events.Messages;

namespace CellSync.Infrastructure.Events;

public class InMemoryMessageQueue
{
    private readonly Channel<IEventMessage> _channel = Channel.CreateUnbounded<IEventMessage>(
        new UnboundedChannelOptions
        {
            SingleReader = true,
            AllowSynchronousContinuations = false
        });

    public async Task EnqueueAsync(IEventMessage message, CancellationToken cancellationToken = default)
    {
        await _channel.Writer.WriteAsync(message, cancellationToken);
    }

    public IAsyncEnumerable<IEventMessage> DequeueAllAsync(CancellationToken cancellationToken = default)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}