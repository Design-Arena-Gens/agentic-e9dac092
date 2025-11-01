using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/team-members")]
public class TeamMembersController : ControllerBase
{
    private readonly ITeamMemberService _teamMemberService;

    public TeamMembersController(ITeamMemberService teamMemberService)
    {
        _teamMemberService = teamMemberService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TeamMemberDto>>> Get(CancellationToken cancellationToken)
    {
        var members = await _teamMemberService.GetAsync(cancellationToken);
        return Ok(members);
    }

    [HttpPost]
    public async Task<ActionResult<TeamMemberDto>> Create([FromBody] CreateTeamMemberRequest request, CancellationToken cancellationToken)
    {
        var member = await _teamMemberService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(Get), new { memberId = member.Id }, member);
    }
}
