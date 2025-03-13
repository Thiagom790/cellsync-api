using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Cell;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class CellRepository(CellSyncDbContext dbContext) : ICellRepository
{
    public async Task AddAsync(Cell cell)
    {
        await dbContext.Cells.AddAsync(cell);
    }

    public async Task<Cell?> GetByIdAsync(Guid cellId)
    {
        var result = await dbContext.Cells.FindAsync(cellId);

        return result;
    }

    public async Task<List<Cell>> GetAllAsync()
    {
        var result = await dbContext.Cells.AsNoTracking().ToListAsync();

        return result;
    }

    public void Update(Cell cell)
    {
        dbContext.Cells.Update(cell);
    }
}