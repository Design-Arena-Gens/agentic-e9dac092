using RCPS.Core.Enums;

namespace RCPS.Core.DTOs;

public record CreateClientRequest(
    string Name,
    string? Industry,
    string? PrimaryContactName,
    string? PrimaryContactEmail,
    CurrencyCode Currency,
    decimal AnnualContractValue);
