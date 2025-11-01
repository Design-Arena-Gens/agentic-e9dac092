namespace RCPS.Core.DTOs;

public record MonthlyFinancialTrendDto(
    int Year,
    int Month,
    decimal RecognizedRevenue,
    decimal InvoicedAmount,
    decimal CostIncurred);
