using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;
        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }
        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Add(Employee employee)
        {
            var result = _employeeDal.Get(e => e.EmployeeId == employee.EmployeeId);
            if (result == null)
            {
                _employeeDal.Add(employee);
                return new SuccessResult(Messages.Employees.Add(employee.FirstName, employee.LastName));
            }
            return new ErrorResult(Messages.Employees.Exists(employee.FirstName, employee.LastName));
        }
        public IResult Delete(Employee employee)
        {
            var result = _employeeDal.Get(c => c.FirstName == employee.FirstName && c.LastName == employee.LastName);
            if (result != null)
            {
                _employeeDal.Delete(employee);
                return new SuccessResult(Messages.Employees.Delete(employee.FirstName, employee.LastName));
            }
            return new ErrorResult(Messages.NotFound);
        }
        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Update(Employee employee)
        {
            var result = _employeeDal.Get(c => c.FirstName == employee.FirstName && c.LastName == employee.LastName);
            if (result != null)
            {
                _employeeDal.Update(employee);
                return new SuccessResult(Messages.Employees.Update(employee.FirstName, employee.LastName));
            }
            return new ErrorResult(Messages.NotFound);
        }
        public IDataResult<Employee> GetById(int employeeId)
        {
            var result = _employeeDal.Get(e => e.EmployeeId == employeeId);
            if (result != null)
            {
                return new SuccessDataResult<Employee>(result);
            }
            return new ErrorDataResult<Employee>(Messages.NotFound);
        }
        public IDataResult<List<Employee>> GetAll()
        {
            var result = _employeeDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Employee>>(result);
            }
            return new ErrorDataResult<List<Employee>>(Messages.NotFound);
        }


    }
}
