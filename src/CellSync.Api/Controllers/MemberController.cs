using CellSync.Application.UseCases.Member.GetAll;
using CellSync.Application.UseCases.Member.GetById;
using CellSync.Application.UseCases.Member.Register;
using CellSync.Application.UseCases.Member.Update;
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
        var response = await useCase.ExecuteAsync();

        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ResponseGetMemberByIdJson), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseGetMemberByIdJson>> GetMemberById(
        [FromRoute] Guid id,
        [FromServices] IGetMemberByIdUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync(id);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(RegisterMemberResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<RegisterMemberResponse>> RegisterMember(
        [FromBody] RegisterMemberRequest registerMemberRequest,
        [FromServices] IRegisterMemberUseCase useCase
    )
    {
        var response = await useCase.Execute(registerMemberRequest);

        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateMember(
        [FromRoute] Guid id,
        [FromBody] RequestUpdateMemberJson request,
        [FromServices] IUpdateMemberUseCase useCase
    )
    {
        await useCase.ExecuteAsync(id, request);

        return NoContent();
    }
}