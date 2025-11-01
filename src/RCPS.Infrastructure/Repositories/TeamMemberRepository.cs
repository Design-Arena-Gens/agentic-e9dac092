using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class TeamMemberRepository : GenericRepository<TeamMember>, ITeamMemberRepository
{
    public TeamMemberRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<TeamMember>> GetActiveAsync(CancellationToken cancellationToken = default)
        => await DbSet.Where(t => !t.IsDeleted)
            .OrderBy(t => t.LastName)
            .ThenBy(t => t.FirstName)
            .ToListAsync(cancellationToken);
}
