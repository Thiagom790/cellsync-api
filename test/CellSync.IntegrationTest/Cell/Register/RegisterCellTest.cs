using System.Net;
using System.Text.Json;
using CellSync.Application.UseCases.Cell.Register;
using FluentAssertions;

namespace CellSync.IntegrationTest.Cell.Register;

public class RegisterCellTest(CustomWebApplicationFactory webApplicationFactory)
    : BaseIntegrationTestFixture(webApplicationFactory)
{
    private const string ENDPOINT = "cell";

    [Fact]
    public async Task RegisterCell_ValidRequest_ReturnsCreatedCellId()
    {
        var request = new RegisterCellRequest
        {
            Name = "Test Cell",
            IsActive = true,
            Address = "123 Test St, Test City, TC 12345",
        };
        
        var response = await DoPost(requestUri: ENDPOINT, request: request);
        
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var body = await response.Content.ReadAsStringAsync();
        var responseJson = JsonDocument.Parse(body);
        var cellId = responseJson.RootElement.GetProperty("id").GetGuid();
        
        cellId.Should().NotBeEmpty();
        
        var cell = await DbContext.Cells.FindAsync(cellId);
        
        cell.Should().NotBeNull();
        cell!.Name.Should().Be(request.Name);
        cell.IsActive.Should().Be(request.IsActive);
        cell.Address.Should().Be(request.Address);
    }
}