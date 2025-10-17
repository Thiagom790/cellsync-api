using System.Data;
using System.Text;
using CellSync.Domain.Entities;
using CellSync.Domain.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Npgsql;

namespace CellSync.Infrastructure.DataAccess.Repositories;

public class BulkInsertRepository(CellSyncDbContext dbContext) : IBulkInsertRepository
{
    public async Task BulkInsertMembersEntityFrameworkAddRangeAsync(IEnumerable<Member> members)
    {
        await dbContext.Members.AddRangeAsync(members);

        await dbContext.SaveChangesAsync();
    }

    public async Task BulkInsertMembersEntityFrameworkChunkAsync(IEnumerable<Member> members)
    {
        const int batchSize = 1000;
        foreach (var chunk in members.Chunk(batchSize))
        {
            dbContext.Members.AddRange(chunk);
            await dbContext.SaveChangesAsync();
            dbContext.ChangeTracker.Clear();
        }
    }

    public async Task BulkInsertMembersDapperAsync(IEnumerable<Member> members)
    {
        var connectionString = dbContext.Database.GetConnectionString();
        await using var sqlConnection = new NpgsqlConnection(connectionString);

        sqlConnection.Open();

        await sqlConnection.BeginTransactionAsync();

        var sql = """
                    INSERT INTO "Members" ("Id", "Name", "Email", "Phone", "ProfileType", "CreatedAt", "UpdatedAt")
                    VALUES (@Id, @Name, @Email, @Phone, @ProfileType, @CreatedAt, @UpdatedAt)
                  """;
        // var sql = "INSERT INTO Members (Id, Name, Email, Phone, ProfileType, CreatedAt, UpdatedAt) " +
        //           "VALUES (@Id, @Name, @Email, @Phone, @ProfileType, @CreatedAt, @UpdatedAt)";

        await sqlConnection.ExecuteAsync(sql, members);
        // await sqlConnection.QueryAsync(sql, members);
    }

    public async Task BulkInsertMembersSqlBulkCopyAsync(IEnumerable<Member> members)
    {
        var cancellationToken = CancellationToken.None;
        var connectionString = dbContext.Database.GetConnectionString();
        await using var connection = new NpgsqlConnection(connectionString);

        await connection.OpenAsync(cancellationToken);

        await using var writer = await connection.BeginBinaryImportAsync(
            """
            COPY "Members" ("Id", "Name", "Email", "Phone", "ProfileType", "CreatedAt", "UpdatedAt")
            FROM STDIN (FORMAT BINARY)
            """, cancellationToken);

        foreach (var member in members)
        {
            await writer.StartRowAsync(cancellationToken);

            await writer.WriteAsync(member.Id, NpgsqlTypes.NpgsqlDbType.Uuid, cancellationToken);
            await writer.WriteAsync(member.Name, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken);
            await writer.WriteAsync(member.Email, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken);
            await writer.WriteAsync(member.Phone, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken);
            await writer.WriteAsync(member.ProfileType, NpgsqlTypes.NpgsqlDbType.Text, cancellationToken);
            await writer.WriteAsync(member.CreatedAt, NpgsqlTypes.NpgsqlDbType.TimestampTz, cancellationToken);
            await writer.WriteAsync(member.UpdatedAt, NpgsqlTypes.NpgsqlDbType.TimestampTz, cancellationToken);
        }

        await writer.CompleteAsync(cancellationToken);
    }

    // SqlServer
    // public async Task BulkInsertMembersSqlBulkCopyAsync(IEnumerable<Member> members)
    // {
    //     var connectionString = dbContext.Database.GetConnectionString();
    //     await using var sqlConnection = new SqlConnection(connectionString);
    //     using var sqlBulkCopy = new SqlBulkCopy(sqlConnection);
    //
    //     sqlBulkCopy.DestinationTableName = "Members";
    //
    //     sqlBulkCopy.ColumnMappings.Add(nameof(Member.Email), "Email");
    //     sqlBulkCopy.ColumnMappings.Add(nameof(Member.Name), "Name");
    //     sqlBulkCopy.ColumnMappings.Add(nameof(Member.Phone), "Phone");
    //     sqlBulkCopy.ColumnMappings.Add(nameof(Member.ProfileType), "ProfileType");
    //     sqlBulkCopy.ColumnMappings.Add(nameof(Member.CreatedAt), "CreatedAt");
    //     sqlBulkCopy.ColumnMappings.Add(nameof(Member.UpdatedAt), "UpdatedAt");
    //     sqlBulkCopy.ColumnMappings.Add(nameof(Member.Id), "Id");
    //
    //     await sqlBulkCopy.WriteToServerAsync(CreateMemberDataTable(members));
    // }
    //
    // private static DataTable CreateMemberDataTable(IEnumerable<Member> members)
    // {
    //     var table = new DataTable();
    //     table.Columns.Add("Id", typeof(Guid));
    //     table.Columns.Add("Name", typeof(string));
    //     table.Columns.Add("Email", typeof(string));
    //     table.Columns.Add("Phone", typeof(string));
    //     table.Columns.Add("ProfileType", typeof(string));
    //     table.Columns.Add("CreatedAt", typeof(DateTime));
    //     table.Columns.Add("UpdatedAt", typeof(DateTime));
    //
    //     foreach (var member in members)
    //     {
    //         var row = table.NewRow();
    //         row["Id"] = member.Id;
    //         row["Name"] = member.Name;
    //         row["Email"] = member.Email ?? (object)DBNull.Value;
    //         row["Phone"] = member.Phone ?? (object)DBNull.Value;
    //         row["ProfileType"] = member.ProfileType;
    //         row["CreatedAt"] = member.CreatedAt;
    //         row["UpdatedAt"] = member.UpdatedAt;
    //
    //         table.Rows.Add(row);
    //     }
    //
    //     return table;
    // }

    public async Task BulkInsertMembersEntityFrameworkRawSqlAsync(IEnumerable<Member> members)
    {
        var sb = new StringBuilder();
        sb.Append("""
                  INSERT INTO "Members" ("Id", "Name", "Email", "Phone", "ProfileType", "CreatedAt", "UpdatedAt") VALUES
                  """);

        var values = members.Select(m =>
            $"('{m.Id}', '{m.Name}', '{m.Email}', '{m.Phone}', '{m.ProfileType}', '{m.CreatedAt:yyyy-MM-dd HH:mm:ss}', '{m.UpdatedAt:yyyy-MM-dd HH:mm:ss}')");

        sb.Append(string.Join(", ", values));
        sb.Append(';');

        await dbContext.Database.ExecuteSqlRawAsync(sb.ToString());
    }
}