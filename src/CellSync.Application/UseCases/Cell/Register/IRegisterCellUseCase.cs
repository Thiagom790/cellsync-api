using CellSync.Communication.Requests;
using CellSync.Communication.Responses;

namespace CellSync.Application.UseCases.Cell.Register;

public interface IRegisterCellUseCase
{
    Task<ResponseRegisterCellJson> ExecuteAsync(RequestRegisterCellJson request);
}