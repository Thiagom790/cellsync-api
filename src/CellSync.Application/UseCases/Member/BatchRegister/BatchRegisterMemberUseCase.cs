using CellSync.Domain.Enums;
using CellSync.Domain.Files;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Member;

namespace CellSync.Application.UseCases.Member.BatchRegister;

public class BatchRegisterMemberUseCase(IMemberRepository repository, IUnitOfWork unitOfWork) : IBatchRegisterMemberUseCase
{
    private readonly List<string> _allowedExtensions = [".csv"];

    public async Task ExecuteAsync(IUploadedFile file)
    {
        if (file.FileSize > (long)AllowedFileSize.TenMb)
        {
            throw new Exception("File size exceeds the maximum allowed limit ofB 10M.");
        }

        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!_allowedExtensions.Contains(fileExtension))
        {
            throw new Exception("Invalid file type. Only CSV files are allowed.");
        }

        await using var stream = file.GetFileContentStream();
        using var reader = new StreamReader(stream);
        using var csv = new CsvHelper.CsvReader(reader, System.Globalization.CultureInfo.InvariantCulture);

        var now = DateTime.UtcNow;
        
        var records = csv.GetRecords<BatchRegisterMember>();

        
        var members = records.Select(requestMember => new Domain.Entities.Member()
        {
            Id = Guid.NewGuid(),
            Name = requestMember.Name,
            Email = requestMember.Email,
            Phone = requestMember.Phone,
            ProfileType = string.IsNullOrWhiteSpace(requestMember.ProfileType)
                ? ProfileTypes.MEMBER
                : requestMember.ProfileType,
            CreatedAt = now,
            UpdatedAt = now
        });
        
        await repository.AddRangeAsync(members);

        await unitOfWork.CommitAsync();
    }
}