using FluentValidation;
using RCPS.Core.DTOs;

namespace RCPS.Core.Validators;

public class CreateChangeRequestRequestValidator : AbstractValidator<CreateChangeRequestRequest>
{
    public CreateChangeRequestRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.EstimatedHours).GreaterThan(0);
        RuleFor(x => x.EstimatedAmount).GreaterThanOrEqualTo(0);
    }
}
