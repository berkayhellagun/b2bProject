using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(supplier => supplier.SupplierGiro)
                .GreaterThan(0);
            RuleFor(sup => sup.SupplierMail)
                .NotEmpty().WithMessage("Email adress is required.")
                .EmailAddress().WithMessage("A valid email adress is required.");
            RuleFor(sup => sup.SupplierTelephoneNumber)
                .NotEmpty();
            RuleFor(sup => sup.SupplierName.Length)
                .GreaterThanOrEqualTo(3);
            RuleFor(sup => sup.SupplierTaxNo.Length)
                .Equal(10); //tax number must be 10 char
        }
    }
}
