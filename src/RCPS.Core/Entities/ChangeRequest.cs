using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class ChangeRequest : BaseEntity
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public decimal EstimatedHours { get; set; }
    public decimal EstimatedAmount { get; set; }
    public ChangeRequestStatus Status { get; set; } = ChangeRequestStatus.Draft;
    public DateTime? DecisionDate { get; set; }
    public string? DecisionNotes { get; set; }
}
