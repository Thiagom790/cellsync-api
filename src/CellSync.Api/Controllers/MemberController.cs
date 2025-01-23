using CellSync.Application.UseCases.Member.GetAll;
using CellSync.Application.UseCases.Member.Register;
using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CellSync.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MemberController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAllMembersJson), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseGetAllMembersJson>> GetAllMembers(
        [FromServices] IGetAllMembersUseCase useCase)
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterMemberJson), StatusCodes.Status201Created)]
    public async Task<ActionResult<ResponseRegisterMemberJson>> RegisterMember(
        [FromBody] RequestRegisterMemberJson request,
        [FromServices] IRegisterMemberUseCase useCase
    )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}