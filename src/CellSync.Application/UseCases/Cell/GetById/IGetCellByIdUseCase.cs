namespace CellSync.Application.UseCases.Cell.GetById;

public interface IGetCellByIdUseCase
{
    Task<GetCellByIdResponse> ExecuteAsync(Guid id);
}