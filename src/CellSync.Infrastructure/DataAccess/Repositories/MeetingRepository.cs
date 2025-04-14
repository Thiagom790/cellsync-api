using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Meeting;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class MeetingRepository(CellSyncDbContext dbContext) : IMeetingRepository
{
    public async Task AddAsync(Meeting meeting) => await dbContext.Meetings.AddAsync(meeting);

    public async Task<List<Meeting>> GetAllAsync() => await dbContext.Meetings
        .Include(meeting => meeting.Members)
        .Include(meeting => meeting.Leader)
        .AsNoTracking()
        .ToListAsync();
}