using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<Order> GetById(int orderId);
        IResult Add(Order order);
        IResult Delete(Order order);
        IResult Update(Order order);
        IDataResult<List<Order>> GetAll();
        IDataResult<List<Order>> GetAllByCustomerId(string customerId);
        IDataResult<List<Order>> GetAllByEmployeeId(int employeeId);

        IDataResult<OrderDetailDto> GetOrderDetail(int orderId);
        IDataResult<List<OrderDetailDto>> GetAllOrderDetails();

    }
}
