using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IReminderService
{
    Task<ReminderDto> ScheduleReminderAsync(CreateReminderRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReminderDto>> GetPendingRemindersAsync(DateTime asOf, CancellationToken cancellationToken = default);
    Task MarkAsSentAsync(Guid reminderId, CancellationToken cancellationToken = default);
}
