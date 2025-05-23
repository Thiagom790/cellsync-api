using System.Data;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Event;
using Dapper;

namespace CellSync.Consumer.DataAccess;

public class EventRepository(IDbConnection dbConnection, IEventRepository eventRepository) : IEventRepository
{
    public Task RegisterExecutedEventsAsync(ExecutedEvent executedEvent)
    {
        var sql = @"
            INSERT INTO ExecutedEvents 
                (EventName, EventDescription, EventTime)
            VALUES (@EventName, @EventDescription, @EventTime)
        ";

        // await dbConnection.Execute(sql, executedEvent);
        return Task.CompletedTask;
    }
}