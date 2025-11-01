using RCPS.Core.DTOs;

namespace RCPS.Core.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardSummaryDto> GetDashboardSummaryAsync(DateTime asOf, CancellationToken cancellationToken = default);
}
