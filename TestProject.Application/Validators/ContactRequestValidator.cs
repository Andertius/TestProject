using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using TestProject.Domain.Requests;

namespace TestProject.Application.Validators
{
    public class ContactRequestValidator : AbstractValidator<ContactRequest>
    {
        public ContactRequestValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();
        }
    }
}
