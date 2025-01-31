using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetAll;

public class GetAllCellsUseCase(
    ICellRepository cellRepository,
    ICellAddressRepository cellAddressRepository
) : IGetAllCellsUseCase
{
    public async Task<ResponseGetAllCellsJson> Execute()
    {
        var result = await cellRepository.GetAll();
        var response = new ResponseGetAllCellsJson();

        foreach (var cell in result)
        {
            var address = await cellAddressRepository.GetCurrentCellAddress(cell.Id);

            response.Cells.Add(new ResponseCellJson
            {
                Id = cell.Id,
                IsActive = cell.IsActive,
                Name = cell.Name,
                CurrentAddress = address is null
                    ? null
                    : new ResponseGetCellAddressJson
                    {
                        Id = address.Id,
                        Address = address.Address
                    },
            });
        }

        return response;
    }
}