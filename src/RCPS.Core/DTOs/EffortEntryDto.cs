using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record EffortEntryDto(
    Guid Id,
    Guid ProjectId,
    Guid TeamMemberId,
    string TeamMemberName,
    EffortType EffortType,
    DateTime WorkDate,
    decimal Hours,
    decimal BillRate,
    decimal CostRate,
    string? Notes);
