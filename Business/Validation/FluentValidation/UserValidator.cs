using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<Person>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty();
            RuleFor(user => user.LastName)
                .NotEmpty();
            RuleFor(user => user.eMail)
                .NotEmpty()
                .EmailAddress();
            RuleFor(user => user.TCVKN)
                .NotEmpty();
        }
    }
}
