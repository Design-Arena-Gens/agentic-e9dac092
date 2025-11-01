using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record CreateProjectRequest(
    string Name,
    Guid ClientId,
    string? Description,
    ProjectStatus Status,
    ProjectHealth Health,
    DateTime StartDate,
    DateTime EndDate,
    decimal BudgetHours,
    decimal BudgetAmount);
