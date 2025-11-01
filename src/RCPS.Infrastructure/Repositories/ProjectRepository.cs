using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Project?> GetFullProjectAsync(Guid id, CancellationToken cancellationToken = default)
        => await DbSet
            .Include(p => p.Client)
            .Include(p => p.Milestones)
            .Include(p => p.StatementsOfWork)
            .Include(p => p.ChangeRequests)
            .Include(p => p.Invoices)
            .ThenInclude(i => i.Lines)
            .Include(p => p.RevenueRecognitions)
            .Include(p => p.Assignments)
            .ThenInclude(a => a.TeamMember)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);

    public async Task<IReadOnlyList<Project>> GetActiveProjectsAsync(CancellationToken cancellationToken = default)
        => await DbSet
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.CreatedAtUtc)
            .ToListAsync(cancellationToken);
}
