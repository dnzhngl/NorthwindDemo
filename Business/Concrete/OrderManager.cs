using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Add(Order order)
        {
            _orderDal.Add(order);
        }
        public void Delete(Order order)
        {
            _orderDal.Delete(order);
        }
        public void Update(Order order)
        {
            _orderDal.Update(order);
        }
        public Order Get(int orderId)
        {
            return _orderDal.Get(o => o.OrderId == orderId);
        }

        public List<Order> GetAll()
        {
            return _orderDal.GetAll();
        }

        public List<Order> GetAllByCustomerId(string customerId)
        {
            return _orderDal.GetAll(o => o.CustomerId == customerId);
        }

        public List<Order> GetAllByEmployeeId(int employeeId)
        {
            return _orderDal.GetAll(o => o.EmployeeId == employeeId);
        }

        public List<OrderDetailDto> GetAllOrderDetails()
        {
            return _orderDal.GetAllOrderDetails();
        }

        public OrderDetailDto GetOrderDetail(int orderId)
        {
            return _orderDal.GetOrderDetail(orderId);
        }

    }
}
