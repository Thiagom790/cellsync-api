using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Cell;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class CellRepository(CellSyncDbContext dbContext) : ICellRepository
{
    public async Task Add(Cell cell)
    {
        await dbContext.Cells.AddAsync(cell);
    }

    public async Task<Cell?> GetByIdWithCurrentAddress(Guid id)
    {
        var result = await dbContext.Cells
            .Include(cell => cell.Addresses.Where(address => address.IsCurrent))
            .AsNoTracking()
            .Where(cell => cell.Id == id)
            .FirstOrDefaultAsync();

        return result;
    }

    public async Task<List<Cell>> GetAllWithCurrentAddress()
    {
        return await dbContext.Cells
            .Include(cell => cell.Addresses.Where(address => address.IsCurrent))
            .AsNoTracking()
            .ToListAsync();
    }
}