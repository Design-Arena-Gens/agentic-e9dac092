using FluentValidation;
using RCPS.Core.DTOs;

namespace RCPS.Core.Validators;

public class CreateEffortEntryRequestValidator : AbstractValidator<CreateEffortEntryRequest>
{
    public CreateEffortEntryRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.TeamMemberId).NotEmpty();
        RuleFor(x => x.WorkDate).NotEmpty();
        RuleFor(x => x.Hours).GreaterThan(0);
        RuleFor(x => x.BillRate).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CostRate).GreaterThanOrEqualTo(0);
    }
}
