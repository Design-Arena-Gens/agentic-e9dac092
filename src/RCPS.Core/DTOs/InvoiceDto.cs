using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record InvoiceDto(
    Guid Id,
    Guid ProjectId,
    string InvoiceNumber,
    InvoiceStatus Status,
    DateTime InvoiceDate,
    DateTime DueDate,
    decimal Subtotal,
    decimal TaxAmount,
    decimal AmountPaid,
    IEnumerable<InvoiceLineDto> Lines);
