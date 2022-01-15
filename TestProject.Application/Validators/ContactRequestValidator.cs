using FluentValidation;

using TestProject.Domain.Requests;

namespace TestProject.Application.Validators
{
    public class ContactRequestValidator : AbstractValidator<ContactRequest>
    {
        public ContactRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(x => $"'{x.Email}' is not a valid email address.");

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
