using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CellSync.Application.UseCases.Member.Register;
using CellSync.Domain.Enums;
using FluentAssertions;

namespace CellSync.IntegrationTest.Member.Register;

public class RegisterMemberTest(CustomWebApplicationFactory webApplicationFactory)
    : BaseIntegrationTestFixture(webApplicationFactory)
{
    private const string ENDPOINT = "member";

    [Fact]
    public async Task RegisterMember_ValidRequest_ReturnsCreatedMemberId()
    {
        var request = new RegisterMemberRequest { Name = "Thiago", ProfileType = ProfileTypes.LEADER };

        var response = await DoPost(requestUri: ENDPOINT, request: request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await response.Content.ReadAsStringAsync();

        var responseJson = JsonDocument.Parse(body);

        var memberId = responseJson.RootElement.GetProperty("id").GetGuid();

        memberId.Should().NotBeEmpty();

        var member = await DbContext.Members.FindAsync(memberId);

        member.Should().NotBeNull();
    }
}