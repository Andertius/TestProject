using FluentValidation;

using TestProject.Domain.Requests;

namespace TestProject.Application.Validators
{
    public sealed class AccountRequestValidator : AbstractValidator<AccountRequest>
    {
        public AccountRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage(x => $"'{x.Email}' is not a valid email address.");

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
