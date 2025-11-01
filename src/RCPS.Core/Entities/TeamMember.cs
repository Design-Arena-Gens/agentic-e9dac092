namespace RCPS.Core.Entities;

public class TeamMember : BaseEntity
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
    public decimal DefaultBillRate { get; set; }
    public decimal DefaultCostRate { get; set; }

    public ICollection<ProjectAssignment> Assignments { get; set; } = new List<ProjectAssignment>();
    public ICollection<EffortEntry> Efforts { get; set; } = new List<EffortEntry>();
}
