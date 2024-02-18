using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;
using Application.Queries;
using Application.DTOs;

[Route("api/[controller]")]
[ApiController]
public class StationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add")]
    public async Task<ActionResult<int>> AddStation([FromBody] AddStationCommand command)
    {
        var stationId = await _mediator.Send(command);
        return Ok(new { StationId = stationId, Message = "Station added successfully." });
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StationDto>>> GetAllStations()
    {
        var stations = await _mediator.Send(new GetAllStationsQuery());
        return Ok(stations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StationDto>> GetStationById(int id)
    {
        var station = await _mediator.Send(new GetStationByIdQuery { StationId = id });
        if (station == null)
        {
            return NotFound($"Station with ID {id} not found.");
        }
        return Ok(station);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStation(int id)
    {
        var result = await _mediator.Send(new DeleteStationCommand { StationId = id });
        if (!result)
        {
            return NotFound($"Station with ID {id} not found or could not be deleted.");
        }
        return Ok($"Station with ID {id} deleted successfully.");
    }
}
