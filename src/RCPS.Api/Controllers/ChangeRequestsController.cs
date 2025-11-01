using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/change-requests")]
public class ChangeRequestsController : ControllerBase
{
    private readonly IChangeRequestService _changeRequestService;

    public ChangeRequestsController(IChangeRequestService changeRequestService)
    {
        _changeRequestService = changeRequestService;
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<ActionResult<IEnumerable<ChangeRequestDto>>> GetByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var changeRequests = await _changeRequestService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(changeRequests);
    }

    [HttpPost]
    public async Task<ActionResult<ChangeRequestDto>> Create([FromBody] CreateChangeRequestRequest request, CancellationToken cancellationToken)
    {
        var changeRequest = await _changeRequestService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByProject), new { projectId = changeRequest.ProjectId }, changeRequest);
    }

    [HttpPut("{changeRequestId:guid}")]
    public async Task<ActionResult<ChangeRequestDto>> UpdateStatus(Guid changeRequestId, [FromBody] ChangeRequestDto request, CancellationToken cancellationToken)
    {
        var changeRequest = await _changeRequestService.UpdateStatusAsync(changeRequestId, request, cancellationToken);
        return changeRequest is null ? NotFound() : Ok(changeRequest);
    }
}
