namespace CellSync.Application.UseCases.Cell.Register;

public interface IRegisterCellUseCase
{
    Task<RegisterCellResponse> ExecuteAsync(RegisterCellRequest registerCellRequest);
}