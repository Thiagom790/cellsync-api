using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.GetById;

public class GetCellByIdUseCase(ICellRepository cellRepository) : IGetCellByIdUseCase
{
    public async Task<GetCellByIdResponse> ExecuteAsync(Guid id)
    {
        var result = await cellRepository.GetByIdAsync(id);

        if (result is null) throw new Exception("Cell not found");

        var response = new GetCellByIdResponse
        {
            Id = result.Id,
            Name = result.Name,
            IsActive = result.IsActive,
            Address = result.Address,
            CreatedAt = result.CreatedAt,
            UpdatedAt = result.UpdatedAt,
            CurrentLeader = result.CurrentLeader is not null
                ? new LeaderResponse
                {
                    Id = result.CurrentLeader.Id,
                    Name = result.CurrentLeader.Name,
                }
                : null,
            LeaderHistory = result.LeaderHistory
                .Select(leader => new LeaderResponse
                {
                    Id = leader.Id,
                    Name = leader.Name,
                }).ToList(),
        };

        return response;
    }
}