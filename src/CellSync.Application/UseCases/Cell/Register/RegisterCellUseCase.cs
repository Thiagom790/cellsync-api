using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.Register;

public class RegisterCellUseCase(ICellRepository cellRepository, IUnitOfWork unitOfWork) : IRegisterCellUseCase
{
    public async Task<ResponseRegisterCellJson> ExecuteAsync(RequestRegisterCellJson request)
    {
        var cell = new Domain.Entities.Cell
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            IsActive = request.IsActive,
            Address = request.Address,
            CurrentLeaderId = request.CurrentLeaderId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        if (request.CurrentLeaderId.HasValue)
        {
            cell.CellLeaderHistory =
            [
                new CellLeaderHistory
                {
                    CellId = cell.Id,
                    LeaderId = request.CurrentLeaderId.Value
                }
            ];
        }

        await cellRepository.AddAsync(cell);
        await unitOfWork.CommitAsync();

        return new ResponseRegisterCellJson { Id = cell.Id };
    }
}