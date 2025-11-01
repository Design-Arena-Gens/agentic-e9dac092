using Microsoft.EntityFrameworkCore;
using RCPS.Core.DTOs;
using RCPS.Core.Enums;
using RCPS.Core.Interfaces;
using RCPS.Infrastructure.Data;

namespace RCPS.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _dbContext;

    public DashboardRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DashboardSummaryDto> GetDashboardSummaryAsync(DateTime asOf, CancellationToken cancellationToken = default)
    {
        var activeProjects = await _dbContext.Projects
            .Where(p => !p.IsDeleted)
            .ToListAsync(cancellationToken);

        var overdueInvoices = await _dbContext.Invoices
            .Where(i => !i.IsDeleted && i.DueDate < asOf && i.AmountPaid < i.Subtotal + i.TaxAmount)
            .ToListAsync(cancellationToken);

        var summary = new DashboardSummaryDto(
            TotalRevenue: activeProjects.Sum(p => p.RevenueRecognized),
            TotalCost: activeProjects.Sum(p => p.CostIncurred),
            GrossMargin: activeProjects.Sum(p => p.RevenueRecognized) - activeProjects.Sum(p => p.CostIncurred),
            ChangeRequestBacklog: await _dbContext.ChangeRequests
                .Where(cr => !cr.IsDeleted && cr.Status != ChangeRequestStatus.Implemented && cr.Status != ChangeRequestStatus.Rejected)
                .SumAsync(cr => cr.EstimatedAmount, cancellationToken),
            ActiveProjects: activeProjects.Count,
            OverdueInvoices: overdueInvoices.Count,
            ProjectHealthBreakdown: Enum.GetValues<ProjectHealth>()
                .Select(health => new ProjectHealthSummaryDto(
                    health,
                    activeProjects.Count(p => p.Health == health)))
                .ToList(),
            MonthlyFinancials: await BuildFinancialTrendsAsync(cancellationToken));

        return summary;
    }

    private async Task<IReadOnlyCollection<MonthlyFinancialTrendDto>> BuildFinancialTrendsAsync(CancellationToken cancellationToken)
    {
        var recognitions = await _dbContext.RevenueRecognitions
            .Where(r => !r.IsDeleted)
            .ToListAsync(cancellationToken);

        var invoices = await _dbContext.Invoices
            .Where(i => !i.IsDeleted)
            .ToListAsync(cancellationToken);

        var costs = await _dbContext.Projects
            .Where(p => !p.IsDeleted)
            .SelectMany(p => p.Efforts)
            .Where(e => !e.IsDeleted)
            .ToListAsync(cancellationToken);

        var months = recognitions
            .Select(r => new { r.RecognitionDate.Year, r.RecognitionDate.Month })
            .Union(invoices.Select(i => new { i.InvoiceDate.Year, i.InvoiceDate.Month }))
            .Union(costs.Select(c => new { c.WorkDate.Year, c.WorkDate.Month }))
            .Distinct()
            .OrderBy(x => x.Year)
            .ThenBy(x => x.Month)
            .TakeLast(12)
            .ToList();

        return months
            .Select(x => new MonthlyFinancialTrendDto(
                x.Year,
                x.Month,
                RecognizedRevenue: recognitions.Where(r => r.RecognitionDate.Year == x.Year && r.RecognitionDate.Month == x.Month).Sum(r => r.Amount),
                InvoicedAmount: invoices.Where(i => i.InvoiceDate.Year == x.Year && i.InvoiceDate.Month == x.Month).Sum(i => i.Subtotal + i.TaxAmount),
                CostIncurred: costs.Where(c => c.WorkDate.Year == x.Year && c.WorkDate.Month == x.Month).Sum(c => c.Hours * c.CostRate)))
            .ToList();
    }
}
