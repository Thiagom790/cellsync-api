using CellSync.Domain.Repositories;

namespace CellSync.Infrastructure.DataAccess;

internal class UnitOfWork(CellSyncDbContext dbContext) : IUnitOfWork
{
    public async Task CommitAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}