using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record CreateRevenueRecognitionRequest(
    Guid ProjectId,
    DateTime RecognitionDate,
    decimal Amount,
    RevenueRecognitionStatus Status,
    string? Notes);
