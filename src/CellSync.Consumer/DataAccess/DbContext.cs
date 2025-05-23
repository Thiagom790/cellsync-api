using System.Data;
using Npgsql;

namespace CellSync.Consumer.DataAccess;

public class DbContext(string connectionString)
{
    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}