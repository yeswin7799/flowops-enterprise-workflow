using FlowOps.API.Models.Requests;
using FlowOps.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlowOps.API.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestsController : ControllerBase
{
    private readonly RequestService _requestService;

    public RequestsController(RequestService requestService)
    {
        _requestService = requestService;
    }

    // POST: api/requests
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequestDto dto)
    {
        try
        {
            var requestId = await _requestService.CreateRequestAsync(
                dto.Title,
                dto.Description,
                dto.Priority,
                dto.CreatedByUserId,
                dto.WorkflowTemplateId
            );

            return CreatedAtAction(nameof(GetById), new { id = requestId }, requestId);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // PUT: api/requests/{id}/submit
    [HttpPut("{id:guid}/submit")]
    public async Task<IActionResult> Submit(Guid id, [FromBody] SubmitRequestDto dto)
    {
        try
        {
            await _requestService.SubmitRequestAsync(id, dto.ChangedByUserId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    // GET: api/requests/{id}
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var request = await _requestService.GetByIdAsync(id);

        if (request is null)
            return NotFound();

        return Ok(request);
    }
}
