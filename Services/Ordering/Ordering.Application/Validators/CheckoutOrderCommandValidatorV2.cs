using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CheckoutOrderCommandValidatorV2 : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidatorV2()
        {
            RuleFor(x => x.UserName)
               .NotEmpty().WithMessage("Username is required.")
               .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(x => x.TotalPrice)
                .NotNull().WithMessage("Total price is required.")
                .GreaterThan(0).WithMessage("Total price must be greater than zero.");
        }
    }
}
