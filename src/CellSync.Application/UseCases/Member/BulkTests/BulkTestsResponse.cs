namespace CellSync.Application.UseCases.Member.BulkTests;

public class BulkTestsResponse
{
    public string Message { get; set; } = string.Empty;
    public long DapperTimeMilliseconds { get; set; }
    public long EntityFrameworkAddRangeTimeMilliseconds { get; set; }
    public long EntityFrameworkChunkTimeMilliseconds { get; set; }
    public long SqlBulkCopyTimeMilliseconds { get; set; }
    public long EntityFrameworkRawSqlTimeMilliseconds { get; set; }
}