using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/efforts")]
public class EffortsController : ControllerBase
{
    private readonly IEffortService _effortService;

    public EffortsController(IEffortService effortService)
    {
        _effortService = effortService;
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<ActionResult<IEnumerable<EffortEntryDto>>> GetByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var efforts = await _effortService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(efforts);
    }

    [HttpPost]
    public async Task<ActionResult<EffortEntryDto>> LogEffort([FromBody] CreateEffortEntryRequest request, CancellationToken cancellationToken)
    {
        var effort = await _effortService.LogEffortAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByProject), new { projectId = effort.ProjectId }, effort);
    }
}
