using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record ReminderDto(
    Guid Id,
    ReminderType Type,
    Guid? ProjectId,
    Guid? InvoiceId,
    DateTime ScheduledFor,
    bool IsSent,
    string? Message);
