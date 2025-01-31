namespace CellSync.Domain.Repositories.Cell;

public interface ICellRepository
{
    Task Add(Entities.Cell cell);

    Task<Entities.Cell?> GetById(Guid id);
    
    Task<List<Entities.Cell>> GetAll();
}