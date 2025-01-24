using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Cell;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class CellRepository(CellSyncDbContext dbContext) : ICellRepository
{
    public async Task Add(Cell cell)
    {
        await dbContext.Cells.AddAsync(cell);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Cell?> GetById(Guid id)
    {
        var cell = await dbContext.Cells.FindAsync(id);

        if (cell is null) return cell;

        var cellAddress = await dbContext.CellAddresses.Where(cellAddress => cellAddress.CellId == cell.Id)
            .FirstOrDefaultAsync();

        cell.CurrentAddress = cellAddress;

        return cell;
    }
}