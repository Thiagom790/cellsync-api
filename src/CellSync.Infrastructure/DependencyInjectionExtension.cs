using CellSync.Domain.Repositories.Cell;
using CellSync.Domain.Repositories.Member;
using CellSync.Infrastructure.DataAccess;
using CellSync.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ICellRepository, CellRepository>();
        services.AddScoped<ICellAddressRepository, CellRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString =
            @"Host=localhost;Port=5432;Username=postgres;Password=@Password123;Database=cellsync;";

        services.AddDbContext<CellSyncDbContext>(config => config.UseNpgsql(connectionString));
    }
}