using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record CreateReminderRequest(
    ReminderType Type,
    Guid? ProjectId,
    Guid? InvoiceId,
    DateTime ScheduledFor,
    string? Message);
