using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ChangeRequestDto(
    Guid Id,
    Guid ProjectId,
    string Title,
    string? Description,
    ChangeRequestStatus Status,
    decimal EstimatedHours,
    decimal EstimatedAmount,
    DateTime CreatedAtUtc,
    DateTime? DecisionDate,
    string? DecisionNotes);
