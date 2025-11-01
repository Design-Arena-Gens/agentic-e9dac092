namespace RCPS.Core.DTOs;

public record TeamMemberDto(
    Guid Id,
    string FirstName,
    string LastName,
    string? Email,
    string? Role,
    decimal DefaultBillRate,
    decimal DefaultCostRate);
