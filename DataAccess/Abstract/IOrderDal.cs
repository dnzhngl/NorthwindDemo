using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        /// <summary>
        /// Brings detailed information of the order corresponding to the entered Id parameter.
        /// </summary>
        /// <param name="orderId">Takes the order id of integer type as a parameter.</param>
        /// <returns>Returns order detail in a type of OrderDetailDto</returns>
        OrderDetailDto GetOrderDetail(int orderId);
        /// <summary>
        /// Gets all of the orders details in a type of OrderDetailDto.
        /// </summary>
        /// <returns>Returns list of orders in a type of OrderDetailDto</returns>
        List<OrderDetailDto> GetAllOrderDetails();
    }
}
