using FluentValidation;

using TestProject.Domain.Requests;

namespace TestProject.Application.Validators
{
    public sealed class AccountRequestValidator : AbstractValidator<AccountRequest>
    {
        public AccountRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
