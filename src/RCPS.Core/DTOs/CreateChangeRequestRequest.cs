namespace RCPS.Core.DTOs;

public record CreateChangeRequestRequest(
    Guid ProjectId,
    string Title,
    string? Description,
    decimal EstimatedHours,
    decimal EstimatedAmount);
