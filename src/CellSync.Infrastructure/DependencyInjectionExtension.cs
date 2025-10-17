using CellSync.Domain.Events.Config;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;
using CellSync.Domain.Repositories.Meeting;
using CellSync.Domain.Repositories.Member;
using CellSync.Infrastructure.DataAccess;
using CellSync.Infrastructure.DataAccess.Repositories;
using CellSync.Infrastructure.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddAEventServices(services);
        AddRepositories(services);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ICellRepository, CellRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
        services.AddScoped<IBulkInsertRepository, BulkInsertRepository>();
    }

    private static void AddAEventServices(IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddHostedService<EventConsumer>();
        services.AddSingleton<InMemoryMessageQueue>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("DataBase:ConnectionString");

        services.AddDbContext<CellSyncDbContext>(config => config.UseNpgsql(connectionString));
    }
}