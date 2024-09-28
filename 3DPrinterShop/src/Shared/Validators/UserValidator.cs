using FluentValidation;
using PrinterShop.Shared.Dtos;

namespace PrinterShop.Shared.Validators;

public class UserValidator : AbstractValidator<UserDto>
{
    public UserValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .IsInEnum()
            .WithMessage("The {PropertyName} must be from the predetermined types.");
        
        RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .MinimumLength(3)
            .WithMessage("The {PropertyName} must be at least 3 characters long.")
            .MaximumLength(15)
            .WithMessage("The {PropertyName} must be between 3 and 15 characters long.");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("The {PropertyName} is required.")
            .EmailAddress()
            .WithMessage("The {PropertyName} must be a valid email address.");
    }
}