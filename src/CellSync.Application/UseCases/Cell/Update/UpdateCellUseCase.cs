using CellSync.Communication.Requests;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.Update;

public class UpdateCellUseCase(ICellRepository cellRepository, IUnitOfWork unitOfWork) : IUpdateCellUseCase
{
    public async Task ExecuteAsync(Guid cellId, RequestUpdateCellJson request)
    {
        var result = await cellRepository.GetByIdAsync(cellId);

        if (result is null) throw new Exception("Cell not found");

        result.Name = request.Name;
        result.IsActive = request.IsActive;
        result.Address = request.Address;
        result.CurrentLeaderId = request.CurrentLeaderId;
        result.UpdatedAt = DateTime.UtcNow;

        if (request.CurrentLeaderId.HasValue)
        {
            result.CellLeaderHistory =
            [
                new CellLeaderHistory
                {
                    CellId = cellId,
                    LeaderId = request.CurrentLeaderId.Value,
                }
            ];
        }

        cellRepository.Update(result);

        await unitOfWork.CommitAsync();
    }
}