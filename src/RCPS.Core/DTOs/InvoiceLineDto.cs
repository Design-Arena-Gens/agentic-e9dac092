namespace RCPS.Core.DTOs;

public record InvoiceLineDto(
    Guid Id,
    string? Description,
    decimal Quantity,
    decimal UnitPrice,
    decimal LineTotal);
