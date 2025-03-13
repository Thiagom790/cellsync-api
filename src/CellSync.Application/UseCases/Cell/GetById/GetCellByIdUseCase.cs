using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetById;

public class GetCellByIdUseCase(ICellRepository cellRepository) : IGetCellByIdUseCase
{
    public async Task<ResponseGetCellByIdJson?> Execute(Guid id)
    {
        var result = await cellRepository.GetByIdWithCurrentAddress(id);

        if (result is null) return null;


        var response = new ResponseGetCellByIdJson
        {
            Id = result.Id,
            Name = result.Name,
            IsActive = result.IsActive
        };

        var cellAddress = result.Addresses.FirstOrDefault();

        if (cellAddress is not null)
        {
            response.CurrentAddress = new ResponseGetCellAddressJson
            {
                Id = cellAddress.Id,
                Address = cellAddress.Address
            };
        }

        return response;
    }
}