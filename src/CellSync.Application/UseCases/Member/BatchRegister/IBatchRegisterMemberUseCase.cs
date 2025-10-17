using CellSync.Domain.Entities;
using CellSync.Domain.Files;

namespace CellSync.Application.UseCases.Member.BatchRegister;

public interface IBatchRegisterMemberUseCase
{
    Task ExecuteAsync(IUploadedFile file);
}