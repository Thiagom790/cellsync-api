using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

public class MembersRepository(CellSyncDbContext dbContext) : IMemberRepository
{
    private readonly CellSyncDbContext _dbDbContext = dbContext;

    public async Task<Member> Add(Member member)
    {
        var entity = await _dbDbContext.Members.AddAsync(member);
        var addedMember = entity.Entity;
        await _dbDbContext.SaveChangesAsync();

        return addedMember;
    }

    public async Task<List<Member>> GetAll()
    {
        var members = await _dbDbContext.Members.AsNoTracking().ToListAsync();
        
        return members;
    }
}