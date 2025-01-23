using CellSync.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Infrastructure;

public static class DatabaseMigrationExtension
{
    public static async Task MigrateDatabaseAsync(this IServiceProvider serviceProvider)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<CellSyncDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}