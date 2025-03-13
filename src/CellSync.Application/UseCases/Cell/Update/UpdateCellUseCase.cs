using CellSync.Communication.Requests;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;

namespace CellSync.Application.UseCases.Cell.Update;

public class UpdateCellUseCase(ICellRepository cellRepository, IUnitOfWork unitOfWork) : IUpdateCellUseCase
{
    public async Task Execute(Guid cellId, RequestUpdateCellJson request)
    {
        var result = await cellRepository.GetById(cellId);

        if (result is null)
        {
            throw new Exception("Cell not found");
        }

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            result.Name = request.Name;
        }

        if (request.IsActive.HasValue)
        {
            result.IsActive = request.IsActive.Value;
        }

        cellRepository.Update(result);

        await unitOfWork.CommitAsync();
    }
}