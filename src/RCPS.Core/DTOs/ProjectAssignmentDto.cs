namespace RCPS.Core.DTOs;

public record ProjectAssignmentDto(
    Guid Id,
    Guid ProjectId,
    Guid TeamMemberId,
    string TeamMemberName,
    decimal AllocatedHours,
    DateTime AllocationStart,
    DateTime AllocationEnd);
