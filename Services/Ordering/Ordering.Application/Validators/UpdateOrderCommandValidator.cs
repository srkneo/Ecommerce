using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be greater than zero."); 

            RuleFor(x => x.UserName)
               .NotEmpty()
               .NotNull()
               .WithMessage("Username is required.")
               .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            RuleFor(x => x.TotalPrice)
                .NotNull().WithMessage("Total price is required.");                

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.");
        }
    }
}
