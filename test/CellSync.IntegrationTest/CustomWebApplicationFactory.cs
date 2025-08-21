using CellSync.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace CellSync.IntegrationTest;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("cellsync")
        .WithUsername("postgres")
        .WithPassword("@Password123")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Usado para configurar a injeção de dependência durante os testes
        builder.ConfigureServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<CellSyncDbContext>));

            if (descriptor is not null) services.Remove(descriptor);

            services.AddDbContext<CellSyncDbContext>(config => config.UseNpgsql(_dbContainer.GetConnectionString()));
        });
    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}