using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetAll;

public class GetAllCellsUseCase(ICellRepository cellRepository) : IGetAllCellsUseCase
{
    public async Task<ResponseGetAllCellsJson> Execute()
    {
        var result = await cellRepository.GetAllWithCurrentAddress();
        var response = new ResponseGetAllCellsJson();

        foreach (var cell in result)
        {
            var cellResponse = new ResponseCellJson
            {
                Id = cell.Id,
                Name = cell.Name,
                IsActive = cell.IsActive
            };

            if (cell.Addresses.Count > 0)
            {
                cellResponse.CurrentAddress = new ResponseGetCellAddressJson
                {
                    Id = cell.Addresses.First().Id,
                    Address = cell.Addresses.First().Address,
                };
            }

            response.Cells.Add(cellResponse);
        }

        return response;
    }
}