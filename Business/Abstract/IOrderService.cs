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

        /// <summary>
        /// Gets the order details that has the given customer id.
        /// </summary>
        /// <param name="customerId">Customer id in type of ineteger</param>
        /// <returns>If founds any matching data, returns SuccessDataResult with the data of OrderDetailDto object, else returns ErrorDataResult with an not found message.</returns>
        IDataResult<List<Order>> GetAllByCustomerId(string customerId);
        /// <summary>
        /// Gets the order details that has the given employee id.
        /// </summary>
        /// <param name="employeeId">Employee id in type of ineteger</param>
        /// <returns>If founds any matching data, returns SuccessDataResult with the data of OrderDetailDto object, else returns ErrorDataResult with an not found message.</returns>
        IDataResult<List<Order>> GetAllByEmployeeId(int employeeId);
        /// <summary>
        /// Gets the order details that has the given order id.
        /// </summary>
        /// <param name="orderId">Order id in type of ineteger</param>
        /// <returns>If founds any matching data, returns SuccessDataResult with the data of OrderDetailDto object, else returns ErrorDataResult with an not found message.</returns>
        IDataResult<OrderDetailDto> GetOrderDetail(int orderId);
        /// <summary>
        /// Gets all of the order details.
        /// </summary>
        /// <returns>If founds any matching data, returns SuccessDataResult with the data of list of OrderDetailDto, else returns ErrorDataResult with an not found message.</returns>
        IDataResult<List<OrderDetailDto>> GetAllOrderDetails();

    }
}
