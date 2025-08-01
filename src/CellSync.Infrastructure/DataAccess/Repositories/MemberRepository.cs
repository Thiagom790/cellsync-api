using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Member;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class MemberRepository(CellSyncDbContext dbContext) : IMemberRepository
{
    public async Task AddAsync(Member member) => await dbContext.Members.AddAsync(member);

    public async Task<List<Member>> GetAllAsync() => await dbContext.Members.AsNoTracking().ToListAsync();

    public async Task<Member?> GetByIdAsync(Guid id) => await dbContext.Members.FindAsync(id);

    public void UpdateAsync(Member member) => dbContext.Members.Update(member);
}