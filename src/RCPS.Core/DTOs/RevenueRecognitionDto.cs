using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record RevenueRecognitionDto(
    Guid Id,
    Guid ProjectId,
    DateTime RecognitionDate,
    decimal Amount,
    RevenueRecognitionStatus Status,
    string? Notes);
