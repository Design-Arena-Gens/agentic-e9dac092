using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class RevenueRecognitionRepository : GenericRepository<RevenueRecognition>, IRevenueRecognitionRepository
{
    public RevenueRecognitionRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<RevenueRecognition>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await DbSet
            .Where(r => r.ProjectId == projectId && !r.IsDeleted)
            .OrderByDescending(r => r.RecognitionDate)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<RevenueRecognition>> GetByMonthAsync(int year, int month, CancellationToken cancellationToken = default)
        => await DbSet
            .Where(r => r.RecognitionDate.Year == year && r.RecognitionDate.Month == month && !r.IsDeleted)
            .ToListAsync(cancellationToken);
}
