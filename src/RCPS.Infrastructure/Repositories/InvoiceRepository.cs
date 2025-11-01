using Microsoft.EntityFrameworkCore;
using RCPS.Core.Entities;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
{
    public InvoiceRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Invoice>> GetOverdueAsync(DateTime asOf, CancellationToken cancellationToken = default)
        => await DbSet
            .Where(i => i.DueDate < asOf && i.AmountPaid < i.Subtotal + i.TaxAmount && !i.IsDeleted)
            .OrderBy(i => i.DueDate)
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<Invoice>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
        => await DbSet
            .Include(i => i.Lines)
            .Where(i => i.ProjectId == projectId && !i.IsDeleted)
            .OrderByDescending(i => i.InvoiceDate)
            .ToListAsync(cancellationToken);
}
