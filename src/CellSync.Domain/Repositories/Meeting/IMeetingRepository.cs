using CellSync.Domain.Entities;

namespace CellSync.Domain.Repositories.Meeting;

public interface IMeetingRepository
{
    Task AddAsync(Entities.Meeting meeting);
}