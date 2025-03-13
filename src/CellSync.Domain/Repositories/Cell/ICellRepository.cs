using CellSync.Domain.Entities;

namespace CellSync.Domain.Repositories.Cell;

public interface ICellRepository
{
    Task AddAsync(Entities.Cell cell);

    Task<Entities.Cell?> GetByIdAsync(Guid cellId);

    Task<List<Entities.Cell>> GetAllAsync();

    void Update(Entities.Cell cell);
}