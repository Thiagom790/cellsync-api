namespace CellSync.Application.UseCases.Cell.Update;

public interface IUpdateCellUseCase
{
    Task ExecuteAsync(Guid cellId, UpdateCellRequest updateCellRequest);
}