using CellSync.Domain.Entities;

namespace CellSync.Domain.Repositories.Event;

public interface IEventRepository
{
    public Task RegisterExecutedEventsAsync(ExecutedEvent events);
}