using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/revenue-recognitions")]
public class RevenueRecognitionsController : ControllerBase
{
    private readonly IRevenueRecognitionService _revenueRecognitionService;

    public RevenueRecognitionsController(IRevenueRecognitionService revenueRecognitionService)
    {
        _revenueRecognitionService = revenueRecognitionService;
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<ActionResult<IEnumerable<RevenueRecognitionDto>>> GetByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var recognitions = await _revenueRecognitionService.GetByProjectAsync(projectId, cancellationToken);
        return Ok(recognitions);
    }

    [HttpPost]
    public async Task<ActionResult<RevenueRecognitionDto>> Create([FromBody] CreateRevenueRecognitionRequest request, CancellationToken cancellationToken)
    {
        var recognition = await _revenueRecognitionService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByProject), new { projectId = recognition.ProjectId }, recognition);
    }
}
