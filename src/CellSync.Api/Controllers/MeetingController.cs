using CellSync.Application.UseCases.Meeting.GetAll;
using CellSync.Application.UseCases.Meeting.Register;
using CellSync.Communication.Requests;
using CellSync.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CellSync.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeetingController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterMeetingJson), StatusCodes.Status201Created)]
    public async Task<ActionResult<ResponseRegisterMeetingJson>> RegisterMeeting(
        [FromBody] RequestRegisterMeetingJson request,
        [FromServices] IRegisterMeetingUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseGetAllMeetingsJson), StatusCodes.Status200OK)]
    public async Task<ActionResult<ResponseGetAllMeetingsJson>> GetAllMeetings(
        [FromServices] IGetAllMeetingsUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync();

        return Ok(response);
    }
}