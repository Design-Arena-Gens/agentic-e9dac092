using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record UpdateProjectRequest(
    string Name,
    string? Description,
    ProjectStatus Status,
    ProjectHealth Health,
    DateTime StartDate,
    DateTime EndDate,
    decimal BudgetHours,
    decimal BudgetAmount);
