using FluentValidation;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Shared.Validators;

public class ComponentValidator : AbstractValidator<ComponentDto>
{
    public ComponentValidator()
    {
        RuleFor(x => x.ComponentType)
            .IsInEnum()
            .WithMessage("The {PropertyName} must be from the predetermined types.");
        
        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("{PropertyName} should not be empty");
        
        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("{PropertyName} should not be empty");
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must be greater than 0");
        
        RuleFor(x => x.Description)
            .MaximumLength(50)
            .WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}