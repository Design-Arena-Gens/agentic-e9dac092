namespace RCPS.Core.DTOs;

public record ProjectMilestoneDto(
    Guid Id,
    string Name,
    DateTime TargetDate,
    DateTime? CompletionDate,
    decimal CompletionPercentage);
