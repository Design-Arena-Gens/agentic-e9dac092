using RCPS.Core.Enums;

namespace RCPS.Core.Entities;

public class Client : BaseEntity
{
    public required string Name { get; set; }
    public string? Industry { get; set; }
    public string? PrimaryContactName { get; set; }
    public string? PrimaryContactEmail { get; set; }
    public CurrencyCode Currency { get; set; } = CurrencyCode.USD;
    public decimal AnnualContractValue { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
