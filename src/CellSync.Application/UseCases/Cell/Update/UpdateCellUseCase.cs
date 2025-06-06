using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.Update;

public class UpdateCellUseCase(ICellRepository cellRepository, IUnitOfWork unitOfWork) : IUpdateCellUseCase
{
    public async Task ExecuteAsync(Guid cellId, UpdateCellRequest updateCellRequest)
    {
        var result = await cellRepository.GetByIdAsync(cellId);

        if (result is null) throw new Exception("Cell not found");

        result.Name = updateCellRequest.Name;
        result.IsActive = updateCellRequest.IsActive;
        result.Address = updateCellRequest.Address;
        result.CurrentLeaderId = updateCellRequest.CurrentLeaderId;
        result.UpdatedAt = DateTime.UtcNow;

        if (updateCellRequest.CurrentLeaderId.HasValue)
        {
            result.CellLeaderHistory =
            [
                new CellLeaderHistory
                {
                    CellId = cellId,
                    LeaderId = updateCellRequest.CurrentLeaderId.Value,
                }
            ];
        }

        cellRepository.Update(result);

        await unitOfWork.CommitAsync();
    }
}