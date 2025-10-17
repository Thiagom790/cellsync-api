using CellSync.Domain.Files;

namespace CellSync.Application.UseCases.Member.BulkTests;

public interface IBulkTestsUseCase
{
    Task<BulkTestsResponse> ExecuteAsync(IUploadedFile file);
}