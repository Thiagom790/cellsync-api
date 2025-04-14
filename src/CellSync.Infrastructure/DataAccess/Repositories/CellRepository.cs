using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Cell;
using Microsoft.EntityFrameworkCore;

namespace CellSync.Infrastructure.DataAccess.Repositories;

internal class CellRepository(CellSyncDbContext dbContext) : ICellRepository
{
    public async Task AddAsync(Cell cell) => await dbContext.Cells.AddAsync(cell);

    public async Task<Cell?> GetByIdAsync(Guid cellId) => await dbContext.Cells
        .Include(cell => cell.CurrentLeader)
        .Include(cell => cell.LeaderHistory)
        .AsTracking()
        .FirstOrDefaultAsync(cell => cell.Id == cellId);

    public async Task<List<Cell>> GetAllAsync() => await dbContext.Cells.AsNoTracking().ToListAsync();

    public void Update(Cell cell) => dbContext.Cells.Update(cell);
}