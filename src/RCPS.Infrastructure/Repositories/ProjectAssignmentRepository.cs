using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class ProjectAssignmentRepository : GenericRepository<ProjectAssignment>, IProjectAssignmentRepository
{
    public ProjectAssignmentRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<ProjectAssignment>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await DbSet
            .Include(a => a.TeamMember)
            .Where(a => a.ProjectId == projectId && !a.IsDeleted)
            .OrderByDescending(a => a.CreatedAtUtc)
            .ToListAsync(cancellationToken);
}
