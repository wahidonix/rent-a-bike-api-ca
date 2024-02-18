using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add")]
        public async Task<ActionResult<int>> AddRental([FromBody] AddRentalCommand command)
        {
            var rentalId = await _mediator.Send(command);
            return Ok(new { RentalId = rentalId, Message = "Rental added successfully." });
        }

        [HttpPost("return")]
        public async Task<IActionResult> ReturnRental([FromBody] ReturnRentalCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest("Failed to return the rental.");
            }
            return Ok("Rental returned successfully.");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetAllRentals()
        {
            var rentals = await _mediator.Send(new GetAllRentalsQuery());
            return Ok(rentals);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RentalDto>> GetRentalById(int id)
        {
            var rental = await _mediator.Send(new GetRentalByIdQuery { RentalId = id });
            if (rental == null)
            {
                return NotFound("Rental not found.");
            }
            return Ok(rental);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetRentalsByUserId(string userId)
        {
            var rentals = await _mediator.Send(new GetRentalsByUserIdQuery(userId));
            if (rentals == null || !rentals.Any())
            {
                return NotFound($"No rentals found for user ID {userId}.");
            }
            return Ok(rentals);
        }

    }

}
