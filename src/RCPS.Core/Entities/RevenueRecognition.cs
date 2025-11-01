using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class RevenueRecognition : BaseEntity
{
    public Guid ProjectId { get; set; }
    public Project? Project { get; set; }
    public DateTime RecognitionDate { get; set; }
    public decimal Amount { get; set; }
    public RevenueRecognitionStatus Status { get; set; } = RevenueRecognitionStatus.Planned;
    public string? Notes { get; set; }
}
