using CellSync.Communication.Requests;

namespace CellSync.Application.UseCases.Cell.Update;

public interface IUpdateCellUseCase
{
    Task Execute(Guid cellId, RequestUpdateCellJson request);
}