using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().Length(2, 10);
            RuleFor(e => e.LastName).NotEmpty().Length(2, 20);
            RuleFor(e => e.Title).NotEmpty().MaximumLength(30);
        }
    }
}
