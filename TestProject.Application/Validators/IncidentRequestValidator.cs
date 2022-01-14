using FluentValidation;

using TestProject.Domain.Requests;

namespace TestProject.Application.Validators
{
    public class IncidentRequestValidator : AbstractValidator<IncidentRequest>
    {
        public IncidentRequestValidator()
        {
            RuleFor(x => x.AccountName)
                .NotEmpty();

            RuleFor(x => x.Description)
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
