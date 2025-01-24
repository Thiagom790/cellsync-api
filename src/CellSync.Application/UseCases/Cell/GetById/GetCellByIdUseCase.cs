using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetById;

public class GetCellByIdUseCase(ICellRepository repository) : IGetCellByIdUseCase
{
    public async Task<ResponseGetCellByIdJson?> Execute(Guid id)
    {
        var result = await repository.GetById(id);

        if (result is null) return null;

        var response = new ResponseGetCellByIdJson
        {
            Id = result.Id,
            Name = result.Name,
            IsActive = result.IsActive,
            CurrentAddress = result.CurrentAddress is null
                ? null
                : new ResponseGetCellAddressJson
                {
                    Id = result.CurrentAddress.Id,
                    Address = result.CurrentAddress.Address
                }
        };

        return response;
    }
}