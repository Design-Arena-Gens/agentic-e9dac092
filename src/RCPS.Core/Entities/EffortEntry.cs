using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class EffortEntry : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public Guid TeamMemberId { get; set; }
    public TeamMember? TeamMember { get; set; }
    public EffortType EffortType { get; set; } = EffortType.Billable;
    public DateTime WorkDate { get; set; }
    public decimal Hours { get; set; }
    public decimal BillRate { get; set; }
    public decimal CostRate { get; set; }
    public string? Notes { get; set; }
}
