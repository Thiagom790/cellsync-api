using CellSync.Application.UseCases.Meeting.GetAll;
using CellSync.Application.UseCases.Meeting.Register;
using Microsoft.AspNetCore.Mvc;

namespace CellSync.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MeetingController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterMeetingResponse), StatusCodes.Status201Created)]
    public async Task<ActionResult<RegisterMeetingResponse>> RegisterMeeting(
        [FromBody] RegisterMeetingRequest registerMeetingRequest,
        [FromServices] IRegisterMeetingUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync(registerMeetingRequest);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetAllMeetingsResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<GetAllMeetingsResponse>> GetAllMeetings(
        [FromServices] IGetAllMeetingsUseCase useCase
    )
    {
        var response = await useCase.ExecuteAsync();

        return Ok(response);
    }
}