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
        AddAzureEventHub(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<ICellRepository, CellRepository>();
        services.AddScoped<IMeetingRepository, MeetingRepository>();
    }

    private static void AddAzureEventHub(IServiceCollection services, IConfiguration configuration)
    {
        var eventHubSettings = configuration.GetSection("AzureEventHub").Get<EventHubSettings>();

        services.AddSingleton<IEventHubSettings>(eventHubSettings!);
        services.AddScoped<IEventPublisher, EventHubPublisher>();
        services.AddHostedService<EventHubConsumer>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>("DataBase:ConnectionString");

        services.AddDbContext<CellSyncDbContext>(config => config.UseNpgsql(connectionString));
    }
}