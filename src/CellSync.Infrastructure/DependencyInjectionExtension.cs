using CellSync.Domain.Events.Config;
using CellSync.Domain.Repositories;
using CellSync.Domain.Repositories.Cell;
using CellSync.Domain.Repositories.Meeting;
using CellSync.Domain.Repositories.Member;
using CellSync.Infrastructure.DataAccess;
using CellSync.Infrastructure.DataAccess.Repositories;
using CellSync.Infrastructure.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CellSync.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IHostApplicationBuilder builder)
    {
        AddDbContext(builder);
        AddAEventServices(builder.Services);
        AddRepositories(builder.Services);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ICellRepository, CellRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
    }

    private static void AddAEventServices(IServiceCollection services)
    {
        services.AddScoped<IEventPublisher, EventPublisher>();
        services.AddHostedService<EventConsumer>();
        services.AddSingleton<InMemoryMessageQueue>();
    }

    private static void AddDbContext(IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<CellSyncDbContext>(connectionName: "cellsync");
    }
}