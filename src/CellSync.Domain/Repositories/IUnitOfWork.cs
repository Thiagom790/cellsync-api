namespace CellSync.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}