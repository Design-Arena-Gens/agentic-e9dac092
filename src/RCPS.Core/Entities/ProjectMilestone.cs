namespace RCPS.Core.Entities;

public class ProjectMilestone : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public required string Name { get; set; }
    public DateTime TargetDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public decimal CompletionPercentage { get; set; }
}
