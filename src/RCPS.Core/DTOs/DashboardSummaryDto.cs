namespace RCPS.Core.DTOs;

public record DashboardSummaryDto(
    decimal TotalRevenue,
    decimal TotalCost,
    decimal GrossMargin,
    decimal ChangeRequestBacklog,
    int ActiveProjects,
    int OverdueInvoices,
    IReadOnlyCollection<ProjectHealthSummaryDto> ProjectHealthBreakdown,
    IReadOnlyCollection<MonthlyFinancialTrendDto> MonthlyFinancials);
