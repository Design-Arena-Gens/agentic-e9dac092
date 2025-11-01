using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class EffortEntryRepository : GenericRepository<EffortEntry>, IEffortEntryRepository
{
    public EffortEntryRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<EffortEntry>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await DbSet
            .Include(e => e.TeamMember)
            .Where(e => e.ProjectId == projectId && !e.IsDeleted)
            .OrderByDescending(e => e.WorkDate)
            .ToListAsync(cancellationToken);
}
