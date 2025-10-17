namespace CellSync.Domain.Repositories;

public interface IBulkInsertRepository
{
    Task BulkInsertMembersEntityFrameworkAddRangeAsync(IEnumerable<Entities.Member> members);

    Task BulkInsertMembersEntityFrameworkChunkAsync(IEnumerable<Entities.Member> members);

    Task BulkInsertMembersDapperAsync(IEnumerable<Entities.Member> members);

    Task BulkInsertMembersSqlBulkCopyAsync(IEnumerable<Entities.Member> members);
    
    Task BulkInsertMembersEntityFrameworkRawSqlAsync(IEnumerable<Entities.Member> members);
}