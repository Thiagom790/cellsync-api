using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Cell;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class CellRepository(CellSyncDbContext dbContext) : ICellRepository, ICellAddressRepository
{
    public async Task<CellAddress?> GetCurrentCellAddress(Guid cellId)
    {
        return await dbContext.CellAddresses
            .Where(cellAddress => cellAddress.CellId == cellId && cellAddress.IsCurrent)
            .FirstOrDefaultAsync();
    }

    public async Task Add(Cell cell)
    {
        await dbContext.Cells.AddAsync(cell);
    }

    public async Task<Cell?> GetById(Guid id)
    {
        return await dbContext.Cells.FindAsync(id);
    }

    public async Task<List<Cell>> GetAll()
    {
        return await dbContext.Cells.AsNoTracking().ToListAsync();
    }
}