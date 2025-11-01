namespace RCPS.Core.DTOs;

public record CreateInvoiceLineRequest(
    string? Description,
    decimal Quantity,
    decimal UnitPrice);
