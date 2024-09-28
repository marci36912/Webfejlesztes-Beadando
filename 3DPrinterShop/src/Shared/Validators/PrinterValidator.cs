using FluentValidation;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Shared.Validators;

public class PrinterValidator : AbstractValidator<PrinterDto>
{
    public PrinterValidator()
    {
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("{PropertyName} should not be empty");
        
        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("{PropertyName} should not be empty");
    }
}