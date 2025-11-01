namespace RCPS.Core.Entities;

public class StatementOfWork : BaseEntity
{
    public required string SOWNumber { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public decimal TotalValue { get; set; }
    public decimal TotalHours { get; set; }
    public string? ScopeSummary { get; set; }
}
