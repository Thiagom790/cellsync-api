using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.Register;

public class RegisterCellUseCase(ICellRepository cellRepository) : IRegisterCellUseCase
{
    public async Task<ResponseRegisterCellJson> Execute(RequestRegisterCellJson request)
    {
        var cell = new Domain.Entities.Cell
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsActive = request.IsActive
        };

        if (request.Address is not null && request.Address.Length > 0)
            cell.CurrentAddress = new CellAddress
            {
                Address = request.Address,
                IsCurrent = true,
                CellId = cell.Id,
                Id = Guid.NewGuid()
            };

        await cellRepository.Add(cell);

        return new ResponseRegisterCellJson { Id = cell.Id };
    }
}