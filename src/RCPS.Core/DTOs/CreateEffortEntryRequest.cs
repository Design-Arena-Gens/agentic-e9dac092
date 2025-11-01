using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record CreateEffortEntryRequest(
    Guid ProjectId,
    Guid TeamMemberId,
    EffortType EffortType,
    DateTime WorkDate,
    decimal Hours,
    decimal BillRate,
    decimal CostRate,
    string? Notes);
