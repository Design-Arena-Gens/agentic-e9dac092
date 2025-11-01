using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync(DateTime asOf, CancellationToken cancellationToken = default);
}
