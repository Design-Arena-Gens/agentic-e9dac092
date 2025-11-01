using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface ITeamMemberService
{
    Task<TeamMemberDto> CreateAsync(CreateTeamMemberRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TeamMemberDto>> GetAsync(CancellationToken cancellationToken = default);
}
