using Microsoft.AspNetCore.Mvc;
using RCPS.Core.DTOs;
using RCPS.Services.Interfaces;

namespace RCPS.Api.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _invoiceService;

    public InvoicesController(IInvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpGet("project/{projectId:guid}")]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetByProject(Guid projectId, CancellationToken cancellationToken)
    {
        var invoices = await _invoiceService.GetProjectInvoicesAsync(projectId, cancellationToken);
        return Ok(invoices);
    }

    [HttpGet("overdue")]
    public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetOverdue([FromQuery] DateTime? asOf, CancellationToken cancellationToken)
    {
        var date = asOf ?? DateTime.UtcNow;
        var invoices = await _invoiceService.GetOverdueInvoicesAsync(date, cancellationToken);
        return Ok(invoices);
    }

    [HttpPost]
    public async Task<ActionResult<InvoiceDto>> Create([FromBody] CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _invoiceService.CreateInvoiceAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetByProject), new { projectId = invoice.ProjectId }, invoice);
    }
}
