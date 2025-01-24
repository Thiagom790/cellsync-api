using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Member;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

public class MembersRepository(CellSyncDbContext dbContext) : IMemberRepository
{
    private readonly CellSyncDbContext _dbDbContext = dbContext;

    public async Task Add(Member member)
    {
        await _dbDbContext.Members.AddAsync(member);
        await _dbDbContext.SaveChangesAsync();
    }

    public async Task<List<Member>> GetAll()
    {
        return await _dbDbContext.Members.AsNoTracking().ToListAsync();
    }
}