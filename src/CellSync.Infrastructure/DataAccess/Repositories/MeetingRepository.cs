using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Meeting;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class MeetingRepository(CellSyncDbContext dbContext) : IMeetingRepository
{
    public async Task AddAsync(Meeting meeting) => await dbContext.Meetings.AddAsync(meeting);

    public async Task AddMemberInMeetingAsync(List<MeetingMember> members) =>
        await dbContext.MeetingMembers.AddRangeAsync(members);
}