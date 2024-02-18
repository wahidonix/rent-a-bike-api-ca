using Application.Commands;
using Application.DTOs;
using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ServiceReportsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ServiceReportsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddServiceReport([FromBody] AddServiceReportCommand command)
    {
        var reportId = await _mediator.Send(command);
        return Ok(new { ReportId = reportId, Message = "Service report added successfully." });
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateServiceReportStatus(int id, [FromBody] UpdateServiceReportStatusCommand command)
    {
        if (id != command.ServiceReportId)
        {
            return BadRequest("Mismatched report ID.");
        }

        var result = await _mediator.Send(command);
        if (!result)
        {
            return NotFound("Service report not found.");
        }
        return Ok("Service report status updated successfully.");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ServiceReportDto>>> GetAllServiceReports()
    {
        var reports = await _mediator.Send(new GetAllServiceReportsQuery());
        return Ok(reports);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceReportDto>> GetServiceReportById(int id)
    {
        var report = await _mediator.Send(new GetServiceReportByIdQuery { ServiceReportId = id });
        if (report == null)
        {
            return NotFound("Service report not found.");
        }
        return Ok(report);
    }
}
