using CellSync.Domain.Entities;

namespace CellSync.Domain.Repositories.Cell;

public interface ICellRepository
{
    Task Add(Entities.Cell cell);

    Task<Entities.Cell?> GetById(Guid cellId);

    Task<Entities.Cell?> GetByIdWithCurrentAddress(Guid id);

    Task<List<Entities.Cell>> GetAllWithCurrentAddress();

    void Update(Entities.Cell cell);

    Task<CellAddress?> GetCurrentCellAddress(Guid cellId);

    Task AddNewCellAddress(CellAddress cellAddress);
}