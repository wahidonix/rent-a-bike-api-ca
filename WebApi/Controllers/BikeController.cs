using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BikesController : ControllerBase
{
    private readonly IMediator _mediator;

    public BikesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Fetch all bikes
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BikeDto>>> GetAll()
    {
        var bikes = await _mediator.Send(new GetAllBikesQuery());
        return Ok(bikes);
    }

    // Fetch a single bike by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<BikeDto>> GetById(int id)
    {
        var bike = await _mediator.Send(new GetBikeByIdQuery(id));
        if (bike == null)
        {
            return NotFound();
        }
        return Ok(bike);
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddBike([FromBody] AddBikeCommand command)
    {
        var bikeId = await _mediator.Send(command);
        if (bikeId <= 0)
        {
            return BadRequest("Could not add the bike.");
        }
        return Ok(new { BikeId = bikeId, Message = "Bike added successfully." });
    }

    [HttpPost("rent")]
    public async Task<IActionResult> RentBike([FromBody] RentBikeCommand command)
    {
        var rentalId = await _mediator.Send(command);
        if (rentalId <= 0)
        {
            return BadRequest("Could not rent the bike.");
        }
        return Ok(new { RentalId = rentalId, Message = "Bike rented successfully." });
    }


    [HttpPost("return")]
    public async Task<IActionResult> ReturnBike(ReturnBikeCommand command)
    {
        var result = await _mediator.Send(command);
        if (!result)
        {
            return BadRequest("Failed to return the bike.");
        }
        return Ok("Bike returned successfully.");
    }

    [HttpPost("report")]
    public async Task<IActionResult> CreateServiceReport(CreateServiceReportCommand command)
    {
        var reportId = await _mediator.Send(command);
        return Ok(new { ReportId = reportId, Message = "Service report created successfully." });
    }
}

