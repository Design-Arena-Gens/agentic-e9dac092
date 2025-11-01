using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ChangeRequestSummaryDto(
    Guid Id,
    string Title,
    ChangeRequestStatus Status,
    decimal EstimatedHours,
    decimal EstimatedAmount,
    DateTime CreatedAtUtc);
