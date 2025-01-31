using CellSync.Domain.Entities;

namespace CellSync.Domain.Repositories.Cell;

public interface ICellAddressRepository
{
    Task<CellAddress?> GetCurrentCellAddress(Guid cellId);
}