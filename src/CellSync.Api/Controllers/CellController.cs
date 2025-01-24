using CellSync.Application.UseCases.Cell.GetById;
using CellSync.Application.UseCases.Cell.Register;
using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CellSync.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CellController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterCellJson), StatusCodes.Status201Created)]
    public async Task<ActionResult<RequestRegisterCellJson>> RegisterCell(
        [FromBody] RequestRegisterCellJson request,
        [FromServices] IRegisterCellUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(ResponseGetCellByIdJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ResponseGetCellByIdJson>> Get([FromRoute] Guid id,
        [FromServices] IGetCellByIdUseCase useCase)
    {
        var response = await useCase.Execute(id);

        if (response is null) return NotFound();

        return Ok(response);
    }
}