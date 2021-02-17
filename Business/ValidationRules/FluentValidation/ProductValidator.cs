using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    // İtenilen herhangi bir entitiy / dto için validator yazılabilir.
    public class ProductValidator : AbstractValidator<Product>  //FluentValidator'den gelen AbstractValidator içerisine kim için kullanacağımızı giriyoruz. 
    {
        // Kurallar constructor içerisine yazılır.
        public ProductValidator()
        {
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.ProductName).MinimumLength(2);

            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);

            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p => p.CategoryId == 1);
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı."); // StartWithA uydurma kuralımız
        }

        private bool StartWithA(string arg)  
        {
            return arg.StartsWith("A"); // True yada false döner. false döner validationdan geçemez.
        }
    }
}
