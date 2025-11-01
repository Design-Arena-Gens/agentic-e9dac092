using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record InvoiceSummaryDto(
    Guid Id,
    string InvoiceNumber,
    InvoiceStatus Status,
    DateTime InvoiceDate,
    DateTime DueDate,
    decimal TotalAmount,
    decimal AmountPaid);
