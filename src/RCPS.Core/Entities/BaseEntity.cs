namespace RCPS.Core.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
    public string CreatedBy { get; set; } = "system";
    public string? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; }
}
