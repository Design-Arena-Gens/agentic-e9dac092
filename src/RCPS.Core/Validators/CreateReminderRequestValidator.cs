using FluentValidation;
using RCPS.Core.DTOs;

namespace RCPS.Core.Validators;

public class CreateReminderRequestValidator : AbstractValidator<CreateReminderRequest>
{
    public CreateReminderRequestValidator()
    {
        RuleFor(x => x.ScheduledFor).GreaterThan(DateTime.UtcNow.AddMinutes(-5));
        RuleFor(x => x.Message).MaximumLength(500);
    }
}
