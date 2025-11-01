using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class ChangeRequestRepository : GenericRepository<ChangeRequest>, IChangeRequestRepository
{
    public ChangeRequestRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<ChangeRequest>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await DbSet
            .Where(cr => cr.ProjectId == projectId && !cr.IsDeleted)
            .OrderByDescending(cr => cr.CreatedAtUtc)
            .ToListAsync(cancellationToken);
}
