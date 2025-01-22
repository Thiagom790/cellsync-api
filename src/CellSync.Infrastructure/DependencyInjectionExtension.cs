﻿using CellSync.Domain.Repositories;
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
        services.AddScoped<IMemberRepository, MembersRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        const string connectionString = @"Host=localhost;Port=5432;Username=postgres;Password=@Password123;Database=cellsync;";

        services.AddDbContext<CellSyncDbContext>(config => config.UseNpgsql(connectionString));
    }
}