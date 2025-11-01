using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IRevenueRecognitionRepository : IGenericRepository<RevenueRecognition>
{
    Task<IReadOnlyList<RevenueRecognition>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RevenueRecognition>> GetByMonthAsync(int year, int month, CancellationToken cancellationToken = default);
}
