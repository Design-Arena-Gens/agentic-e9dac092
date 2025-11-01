using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IReminderRepository : IGenericRepository<Reminder>
{
    Task<IReadOnlyList<Reminder>> GetPendingRemindersAsync(DateTime asOf, CancellationToken cancellationToken = default);
}
