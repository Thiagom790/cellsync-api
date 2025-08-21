using System.Net;
using System.Text;
using System.Text.Json;
using CellSync.Application.UseCases.Member.GetById;
using FluentAssertions;

namespace CellSync.IntegrationTest.Member.GetById;

public class GetMemberByIdTest(CustomWebApplicationFactory webApplicationFactory)
    : BaseIntegrationTestFixture(webApplicationFactory)
{
    private const string METHOD = "member";

    [Fact]
    public async Task GetMemberById_ValidRequest_ReturnsMember()
    {
        var testMember = DataSeeder.GetTestMember();

        var response = await DoGet(requestUri: $"{METHOD}/{testMember.Id}");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await response.Content.ReadAsByteArrayAsync();
        var json = Encoding.UTF8.GetString(body);
        var member = JsonSerializer.Deserialize<GetMemberByIdResponse>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        member!.Id.Should().Be(testMember.Id);
        member.Name.Should().Be(testMember.Name);
        member.ProfileType.Should().Be(testMember.ProfileType);
    }
}