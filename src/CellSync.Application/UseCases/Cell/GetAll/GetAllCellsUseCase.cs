using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetAll;

public class GetAllCellsUseCase(ICellRepository cellRepository) : IGetAllCellsUseCase
{
    public async Task<GetAllCellsResponse> ExecuteAsync()
    {
        var result = await cellRepository.GetAllAsync();

        var response = new GetAllCellsResponse
        {
            Cells = result.Select(cell => new CellResponse
            {
                Id = cell.Id,
                Name = cell.Name,
                Address = cell.Address,
                IsActive = cell.IsActive,
                CurrentLeaderId = cell.CurrentLeaderId,
                CreatedAt = cell.CreatedAt,
                UpdatedAt = cell.UpdatedAt,
            }).ToList()
        };

        return response;
    }
}