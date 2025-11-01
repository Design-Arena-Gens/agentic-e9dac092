using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ProjectDto(
    Guid Id,
    string Name,
    Guid ClientId,
    string ClientName,
    string? Description,
    ProjectStatus Status,
    ProjectHealth Health,
    DateTime StartDate,
    DateTime EndDate,
    decimal BudgetHours,
    decimal BudgetAmount,
    decimal ActualHours,
    decimal ActualAmount,
    decimal RevenueRecognized,
    decimal CostIncurred,
    decimal GrossMargin,
    IEnumerable<ProjectMilestoneDto> Milestones,
    IEnumerable<StatementOfWorkDto> StatementsOfWork,
    IEnumerable<ChangeRequestSummaryDto> ChangeRequests,
    IEnumerable<InvoiceSummaryDto> Invoices);
