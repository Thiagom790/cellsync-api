using CellSync.Api.Adapters;
using CellSync.Application.UseCases.Member.BatchRegister;
using CellSync.Application.UseCases.Member.GetAll;
using CellSync.Application.UseCases.Member.GetById;
using CellSync.Application.UseCases.Member.Register;
using CellSync.Application.UseCases.Member.Update;
using CellSync.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CellSync.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MemberController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(GetAllMembersResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetAllMembersResponse>> GetAllMembers(
        [FromServices] IGetAllMembersUseCase useCase)
    {
        var response = await useCase.ExecuteAsync();

        return Ok(response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(GetMemberByIdResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetMemberByIdResponse>> GetMemberById(
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
        var response = await useCase.ExecuteAsync(registerMemberRequest);

        return Created(string.Empty, response);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateMember(
        [FromRoute] Guid id,
        [FromBody] UpdateMemberRequest updateMemberRequest,
        [FromServices] IUpdateMemberUseCase useCase
    )
    {
        await useCase.ExecuteAsync(id, updateMemberRequest);

        return NoContent();
    }

    [HttpPost]
    [Route("batch-register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> BatchRegister(IFormFile file,
        [FromServices] IBatchRegisterMemberUseCase useCase)
    {
        var request = new FormFileAdapter(file);

        await useCase.ExecuteAsync(request);

        return Created(string.Empty, null);
    }
}