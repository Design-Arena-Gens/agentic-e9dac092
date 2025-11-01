namespace RCPS.Core.DTOs;

public record StatementOfWorkDto(
    Guid Id,
    string SOWNumber,
    DateTime EffectiveDate,
    DateTime ExpiryDate,
    decimal TotalValue,
    decimal TotalHours,
    string? ScopeSummary);
