using FluentValidation;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Shared.Validators;

public class OrderValidator : AbstractValidator<OrderDto>
{
    public OrderValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithMessage("{PropertyName} should not be empty");
        
        RuleFor(x => x.PrinterId)
            .NotEmpty()
            .When(y => y.Components is null)
            .WithMessage("{PropertyName} should not be empty");

        RuleFor(x => x.Components)
            .NotEmpty()
            .When(y => y.PrinterId.Equals(Guid.Empty))
            .WithMessage("{PropertyName} should not be empty");
    }
}