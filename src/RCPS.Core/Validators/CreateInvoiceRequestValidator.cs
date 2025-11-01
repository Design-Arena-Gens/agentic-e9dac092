using FluentValidation;
using RCPS.Core.DTOs;

namespace RCPS.Core.Validators;

public class CreateInvoiceRequestValidator : AbstractValidator<CreateInvoiceRequest>
{
    public CreateInvoiceRequestValidator()
    {
        RuleFor(x => x.ProjectId).NotEmpty();
        RuleFor(x => x.InvoiceNumber).NotEmpty().MaximumLength(100);
        RuleFor(x => x.InvoiceDate).NotEmpty();
        RuleFor(x => x.DueDate).GreaterThan(x => x.InvoiceDate);
        RuleFor(x => x.Lines).NotEmpty();
        RuleForEach(x => x.Lines).SetValidator(new CreateInvoiceLineRequestValidator());
    }

    private class CreateInvoiceLineRequestValidator : AbstractValidator<CreateInvoiceLineRequest>
    {
        public CreateInvoiceLineRequestValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0);
        }
    }
}
