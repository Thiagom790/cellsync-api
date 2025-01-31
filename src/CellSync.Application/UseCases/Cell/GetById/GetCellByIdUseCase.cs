using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetById;

public class GetCellByIdUseCase(ICellRepository cellRepository, ICellAddressRepository cellAddressRepository)
    : IGetCellByIdUseCase
{
    public async Task<ResponseGetCellByIdJson?> Execute(Guid id)
    {
        var result = await cellRepository.GetById(id);

        if (result is null) return null;


        var response = new ResponseGetCellByIdJson
        {
            Id = result.Id,
            Name = result.Name,
            IsActive = result.IsActive
        };

        var cellAddress = await cellAddressRepository.GetCurrentCellAddress(result.Id);

        if (cellAddress is not null)
            response.CurrentAddress = new ResponseGetCellAddressJson
            {
                Id = cellAddress.Id,
                Address = cellAddress.Address
            };

        return response;
    }
}