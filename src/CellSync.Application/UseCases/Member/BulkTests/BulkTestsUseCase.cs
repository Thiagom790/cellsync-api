using System.Diagnostics;
using CellSync.Domain.Enums;
using CellSync.Domain.Files;
using CellSync.Domain.Repositories;

namespace CellSync.Application.UseCases.Member.BulkTests;

public class BulkTestsUseCase(IBulkInsertRepository bulkInsertRepository) : IBulkTestsUseCase
{
    private readonly List<string> _allowedExtensions = [".csv"];

    public async Task<BulkTestsResponse> ExecuteAsync(IUploadedFile file)
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

        var records = csv.GetRecords<BulkTestsMember>();

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

        // var dapperTime = await InsertDapperAsync(members);
        // var efAddRangeTime = await InsertEntityFrameworkAsync(members);
        // var efChunkTime = await InsertEntityFrameworkChunkAsync(members);
        var sqlBulkCopyTime = await InsertSqlBulkCopyAsync(members);
        // var efRawSqlTime = await InsertEntityFrameworkRawSqlAsync(members);

        return new BulkTestsResponse
        {
            Message = "Bulk insert operations completed.",
            // DapperTimeMilliseconds = dapperTime,
            // EntityFrameworkAddRangeTimeMilliseconds = efAddRangeTime,
            // EntityFrameworkChunkTimeMilliseconds = efChunkTime,
            SqlBulkCopyTimeMilliseconds = sqlBulkCopyTime,
            // EntityFrameworkRawSqlTimeMilliseconds = efRawSqlTime
        };
    }

    private async Task<long> InsertDapperAsync(IEnumerable<Domain.Entities.Member> members)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await bulkInsertRepository.BulkInsertMembersDapperAsync(members);
        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    private async Task<long> InsertEntityFrameworkAsync(IEnumerable<Domain.Entities.Member> members)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await bulkInsertRepository.BulkInsertMembersEntityFrameworkAddRangeAsync(members);
        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    private async Task<long> InsertEntityFrameworkRawSqlAsync(IEnumerable<Domain.Entities.Member> members)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await bulkInsertRepository.BulkInsertMembersEntityFrameworkRawSqlAsync(members);
        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    private async Task<long> InsertEntityFrameworkChunkAsync(IEnumerable<Domain.Entities.Member> members)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await bulkInsertRepository.BulkInsertMembersEntityFrameworkChunkAsync(members);
        sw.Stop();

        return sw.ElapsedMilliseconds;
    }

    private async Task<long> InsertSqlBulkCopyAsync(IEnumerable<Domain.Entities.Member> members)
    {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        await bulkInsertRepository.BulkInsertMembersSqlBulkCopyAsync(members);
        sw.Stop();

        return sw.ElapsedMilliseconds;
    }
}