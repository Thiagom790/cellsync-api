using System.Net.Http.Json;
using CellSync.Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;

namespace CellSync.IntegrationTest;

public class BaseIntegrationTestFixture : IClassFixture<CustomWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _httpClient;
    protected readonly CellSyncDbContext DbContext;
    protected readonly TestDataSeeder DataSeeder;

    protected BaseIntegrationTestFixture(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();

        var scope = webApplicationFactory.Services.CreateScope();

        DbContext = scope.ServiceProvider.GetRequiredService<CellSyncDbContext>();

        DataSeeder = new TestDataSeeder(DbContext);
    }

    protected async Task<HttpResponseMessage> DoPost(string requestUri, object request) =>
        await _httpClient.PostAsJsonAsync(requestUri, request);

    protected async Task<HttpResponseMessage> DoGet(string requestUri) =>
        await _httpClient.GetAsync(requestUri);

    public Task InitializeAsync()
    {
        return DataSeeder.SeedDataAsync();
    }
    
    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}