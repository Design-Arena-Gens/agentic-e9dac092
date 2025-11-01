using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class Reminder : BaseEntity
{
    public ReminderType Type { get; set; }
    public Guid? ProjectId { get; set; }
    public Project? Project { get; set; }
    public Guid? InvoiceId { get; set; }
    public Invoice? Invoice { get; set; }
    public DateTime ScheduledFor { get; set; }
    public bool IsSent { get; set; }
    public string? Message { get; set; }
}
