using CellSync.Application.UseCases.Cell.GetAll;
using CellSync.Application.UseCases.Cell.GetById;
using CellSync.Application.UseCases.Cell.Register;
using CellSync.Application.UseCases.Cell.Update;
using Microsoft.AspNetCore.Mvc;

namespace CellSync.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CellController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterCellResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<RegisterCellRequest>> RegisterCell(
        [FromBody] RegisterCellRequest registerCellRequest,
        [FromServices] IRegisterCellUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync(registerCellRequest);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(GetCellByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetCellByIdResponse>> GetCellById(
        [FromRoute] Guid id,
        [FromServices] IGetCellByIdUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync(id);

        return Ok(response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAllCellsResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetAllCellsResponse>> GetAllCells(
        [FromServices] IGetAllCellsUseCase useCase)
    {
        var response = await useCase.ExecuteAsync();

        return Ok(response);
    }

    [HttpPut]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateCell(
        [FromServices] IUpdateCellUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] UpdateCellRequest updateCellRequest
    )
    {
        await useCase.ExecuteAsync(id, updateCellRequest);

        return NoContent();
    }
}