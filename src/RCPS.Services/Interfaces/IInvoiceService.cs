using RCPS.Core.DTOs;

namespace RCPS.Services.Interfaces;

public interface IInvoiceService
{
    Task<InvoiceDto> CreateInvoiceAsync(CreateInvoiceRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InvoiceDto>> GetProjectInvoicesAsync(Guid projectId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InvoiceDto>> GetOverdueInvoicesAsync(DateTime asOf, CancellationToken cancellationToken = default);
}
