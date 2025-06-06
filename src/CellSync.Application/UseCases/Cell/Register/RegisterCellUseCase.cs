using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.Register;

public class RegisterCellUseCase(ICellRepository cellRepository, IUnitOfWork unitOfWork) : IRegisterCellUseCase
{
    public async Task<RegisterCellResponse> ExecuteAsync(RegisterCellRequest registerCellRequest)
    {
        var cell = new Domain.Entities.Cell
        {
            Id = Guid.NewGuid(),
            Name = registerCellRequest.Name,
            IsActive = registerCellRequest.IsActive,
            Address = registerCellRequest.Address,
            CurrentLeaderId = registerCellRequest.CurrentLeaderId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        if (registerCellRequest.CurrentLeaderId.HasValue)
        {
            cell.CellLeaderHistory =
            [
                new CellLeaderHistory
                {
                    CellId = cell.Id,
                    LeaderId = registerCellRequest.CurrentLeaderId.Value
                }
            ];
        }

        await cellRepository.AddAsync(cell);
        await unitOfWork.CommitAsync();

        return new RegisterCellResponse { Id = cell.Id };
    }
}