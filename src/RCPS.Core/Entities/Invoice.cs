using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class Invoice : BaseEntity
{
    public required string InvoiceNumber { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;
    public decimal Subtotal { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount => Subtotal + TaxAmount;
    public decimal AmountPaid { get; set; }
    public string? Notes { get; set; }

    public ICollection<InvoiceLine> Lines { get; set; } = new List<InvoiceLine>();
}
