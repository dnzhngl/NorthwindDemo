using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;
        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Add(Customer customer)
        {
            var result = _customerDal.Get(c => c.CompanyName == customer.CompanyName && c.City == customer.City);
            if (result == null)
            {
                _customerDal.Add(customer);
                return new SuccessResult(Messages.Customers.Add(customer.CompanyName, customer.City));
            }
            return new ErrorResult(Messages.Customers.Exists(customer.CompanyName, customer.City));
        }
        public IResult Delete(Customer customer)
        {
            var result = _customerDal.Get(c => c.CompanyName == customer.CompanyName && c.City == customer.City);
            if (result != null)
            {
                _customerDal.Delete(customer);
                return new SuccessResult(Messages.Customers.Delete(customer.CompanyName, customer.City));
            }
            return new ErrorResult(Messages.NotFound);
        }
        public IResult Update(Customer customer)
        {
            var result = _customerDal.Get(c => c.CompanyName == customer.CompanyName && c.City == customer.City);
            if (result != null)
            {
                _customerDal.Update(customer);
                return new SuccessResult(Messages.Customers.Update(customer.CompanyName, customer.City));
            }
            return new ErrorResult(Messages.NotFound);
        }
        public IDataResult<Customer> GetById(string customerId)
        {
            var result = _customerDal.Get(c => c.CustomerId == customerId);
            if (result != null)
            {
                return new SuccessDataResult<Customer>(result);
            }
            return new ErrorDataResult<Customer>(Messages.NotFound);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Customer>>(result);
            }
            return new ErrorDataResult<List<Customer>>(Messages.NotFound);
        }


    }
}
