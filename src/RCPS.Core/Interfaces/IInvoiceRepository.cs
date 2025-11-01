using RCPS.Core.Entities;

namespace RCPS.Core.Interfaces;

public interface IInvoiceRepository : IGenericRepository<Invoice>
{
    Task<IReadOnlyList<Invoice>> GetOverdueAsync(DateTime asOf, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Invoice>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default);
}
