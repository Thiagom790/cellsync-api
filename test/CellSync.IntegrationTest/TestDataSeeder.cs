using CellSync.Domain.Enums;
using CellSync.Infrastructure.DataAccess;

namespace CellSync.IntegrationTest;

public class TestDataSeeder(CellSyncDbContext dbContext)
{
    private readonly List<Domain.Entities.Member> _members = [];

    public async Task SeedDataAsync()
    {
        await SeedMembers();
        await dbContext.SaveChangesAsync();
    }

    private async Task SeedMembers()
    {
        var member1 = new Domain.Entities.Member()
        {
            Id = Guid.NewGuid(),
            Name = "Test Member 1",
            ProfileType = ProfileTypes.MEMBER,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var member2 = new Domain.Entities.Member()
        {
            Id = Guid.NewGuid(),
            Name = "Test Member 2",
            ProfileType = ProfileTypes.LEADER,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _members.Add(member1);
        _members.Add(member2);

        await dbContext.Members.AddRangeAsync([member1, member2]);
    }

    public Domain.Entities.Member GetTestMember(int index = 0) => _members[index];
}