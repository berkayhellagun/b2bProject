using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(product => product.ProductPrice)
                .GreaterThan(0);
            RuleFor(product => product.ProductName.Length)
                .GreaterThanOrEqualTo(2);
            RuleFor(product => product.ProductSupplierId)
                .NotNull()
                .NotEmpty();
            RuleFor(product => product.ProductSubCategoryId)
                .NotNull()
                .NotEmpty();
            RuleFor(product => product.ProductCountry)
                .NotEmpty();
            RuleFor(product => product.ProductName.Length)
                .GreaterThanOrEqualTo(2);
        }
    }
}
