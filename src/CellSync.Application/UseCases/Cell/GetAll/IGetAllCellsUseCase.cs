namespace CellSync.Application.UseCases.Cell.GetAll;

public interface IGetAllCellsUseCase
{
    Task<GetAllCellsResponse> ExecuteAsync();
}