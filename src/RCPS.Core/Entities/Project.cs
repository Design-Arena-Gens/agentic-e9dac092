using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class Project : BaseEntity
{
    public required string Name { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Draft;
    public ProjectHealth Health { get; set; } = ProjectHealth.OnTrack;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal BudgetHours { get; set; }
    public decimal BudgetAmount { get; set; }
    public decimal ActualHours { get; set; }
    public decimal ActualAmount { get; set; }
    public decimal RevenueRecognized { get; set; }
    public decimal CostIncurred { get; set; }
    public decimal GrossMargin => BudgetAmount == 0 ? 0 : (RevenueRecognized - CostIncurred) / BudgetAmount;

    public ICollection<ProjectMilestone> Milestones { get; set; } = new List<ProjectMilestone>();
    public ICollection<StatementOfWork> StatementsOfWork { get; set; } = new List<StatementOfWork>();
    public ICollection<ProjectAssignment> Assignments { get; set; } = new List<ProjectAssignment>();
    public ICollection<EffortEntry> Efforts { get; set; } = new List<EffortEntry>();
    public ICollection<ChangeRequest> ChangeRequests { get; set; } = new List<ChangeRequest>();
    public ICollection<RevenueRecognition> RevenueRecognitions { get; set; } = new List<RevenueRecognition>();
    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
