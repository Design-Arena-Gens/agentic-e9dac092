using FluentValidation;
using RCPS.Core.DTOs;

namespace RCPS.Core.Validators;

public class CreateRevenueRecognitionRequestValidator : AbstractValidator<CreateRevenueRecognitionRequest>
{
    public CreateRevenueRecognitionRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.RecognitionDate).NotEmpty();
    }
}
