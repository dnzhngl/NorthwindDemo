using Business.Abstract;
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
        public void Add(Customer customer)
        {
            _customerDal.Add(customer);
        }
        public void Delete(Customer customer)
        {
            try
            {
                _customerDal.Delete(customer);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(Customer customer)
        {
            _customerDal.Update(customer);
        }
        public Customer GetById(string customerId)
        {
            return _customerDal.Get(c => c.CustomerId == customerId);
        }

        public List<Customer> GetAll()
        {
            return _customerDal.GetAll();
        }


    }
}
