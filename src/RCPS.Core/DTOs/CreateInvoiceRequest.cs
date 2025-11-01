namespace RCPS.Core.DTOs;

public record CreateInvoiceRequest(
    Guid ProjectId,
    string InvoiceNumber,
    DateTime InvoiceDate,
    DateTime DueDate,
    decimal TaxAmount,
    IEnumerable<CreateInvoiceLineRequest> Lines);
