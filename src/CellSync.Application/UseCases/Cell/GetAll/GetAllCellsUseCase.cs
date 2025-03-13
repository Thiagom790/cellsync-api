using CellSync.Communication.Responses;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetAll;

public class GetAllCellsUseCase(ICellRepository cellRepository) : IGetAllCellsUseCase
{
    public async Task<ResponseGetAllCellsJson> ExecuteAsync()
    {
        var result = await cellRepository.GetAllAsync();
        var cellsResponse = result
            .Select(cell => new ResponseCellJson
            {
                Id = cell.Id,
                Name = cell.Name,
                Address = cell.Address,
                IsActive = cell.IsActive,
            }).ToList();

        var response = new ResponseGetAllCellsJson
        {
            Cells = cellsResponse
        };

        return response;
    }
}