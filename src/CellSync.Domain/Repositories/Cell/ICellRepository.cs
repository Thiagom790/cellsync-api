namespace CellSync.Domain.Repositories.Cell;

public interface ICellRepository
{
    Task Add(Entities.Cell cell);

    Task<Entities.Cell?> GetByIdWithCurrentAddress(Guid id);
    
    Task<List<Entities.Cell>> GetAllWithCurrentAddress();
}