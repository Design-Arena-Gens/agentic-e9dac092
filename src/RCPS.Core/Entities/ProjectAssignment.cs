namespace RCPS.Core.Entities;

public class ProjectAssignment : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public Guid TeamMemberId { get; set; }
    public TeamMember? TeamMember { get; set; }
    public decimal AllocatedHours { get; set; }
    public DateTime AllocationStart { get; set; }
    public DateTime AllocationEnd { get; set; }
}
