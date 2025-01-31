using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Cell.GetAll;

public interface IGetAllCellsUseCase
{
    Task<ResponseGetAllCellsJson> Execute();
}