using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetById;

public class GetCellByIdUseCase(ICellRepository cellRepository) : IGetCellByIdUseCase
{
    public async Task<ResponseGetCellByIdJson> ExecuteAsync(Guid id)
    {
        var result = await cellRepository.GetByIdAsync(id);

        if (result is null) throw new Exception("Cell not found");

        var response = new ResponseGetCellByIdJson
        {
            Id = result.Id,
            Name = result.Name,
            IsActive = result.IsActive,
            Address = result.Address
        };

        return response;
    }
}