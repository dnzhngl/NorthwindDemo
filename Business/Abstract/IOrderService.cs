using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        Order GetById(int orderId);
        void Add(Order order);
        void Delete(Order order);
        void Update(Order order);
        List<Order> GetAll();
        List<Order> GetAllByCustomerId(string customerId);
        List<Order> GetAllByEmployeeId(int employeeId);

        OrderDetailDto GetOrderDetail(int orderId);
        List<OrderDetailDto> GetAllOrderDetails();

    }
}
