using RCPS.Core.DTOs;
using RCPS.Core.Interfaces;
using RCPS.Services.Interfaces;

namespace RCPS.Services.Implementations;

public class DashboardService : IDashboardService
{
    private readonly IDashboardRepository _dashboardRepository;

    public DashboardService(IDashboardRepository dashboardRepository)
    {
        _dashboardRepository = dashboardRepository;
    }

    public Task<DashboardSummaryDto> GetSummaryAsync(DateTime asOf, CancellationToken cancellationToken = default)
        => _dashboardRepository.GetDashboardSummaryAsync(asOf, cancellationToken);
}
