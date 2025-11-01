using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class ReminderRepository : GenericRepository<Reminder>, IReminderRepository
{
    public ReminderRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Reminder>> GetPendingRemindersAsync(DateTime asOf, CancellationToken cancellationToken = default)
        => await DbSet
            .Where(r => !r.IsSent && r.ScheduledFor <= asOf && !r.IsDeleted)
            .OrderBy(r => r.ScheduledFor)
            .ToListAsync(cancellationToken);
}
