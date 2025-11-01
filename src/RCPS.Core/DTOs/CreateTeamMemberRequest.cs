namespace RCPS.Core.DTOs;

public record CreateTeamMemberRequest(
    string FirstName,
    string LastName,
    string? Email,
    string? Role,
    decimal DefaultBillRate,
    decimal DefaultCostRate);
