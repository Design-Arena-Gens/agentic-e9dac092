using FluentValidation;
using RCPS.Core.DTOs;

namespace RCPS.Core.Validators;

public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.ClientId).NotEmpty();
        RuleFor(x => x.StartDate).LessThan(x => x.EndDate);
        RuleFor(x => x.BudgetHours).GreaterThanOrEqualTo(0);
        RuleFor(x => x.BudgetAmount).GreaterThanOrEqualTo(0);
    }
}
