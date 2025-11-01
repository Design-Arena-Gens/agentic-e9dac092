using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface ITeamMemberRepository : IGenericRepository<TeamMember>
{
    Task<IReadOnlyList<TeamMember>> GetActiveAsync(CancellationToken cancellationToken = default);
}
