using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, NorthwindContext>, IOrderDal
    {
        public OrderDetailDto GetOrderDetail(int orderId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {

                var result = from o in context.Orders
                             join e in context.Employees on o.EmployeeId equals e.EmployeeId
                             join c in context.Customers on o.CustomerId equals c.CustomerId
                             where o.OrderId == orderId
                             select new OrderDetailDto
                             {
                                 OrderId = o.OrderId,
                                 CompanyName = c.CompanyName,
                                 EmployeeName = $"{e.FirstName} {e.LastName}",
                                 OrderDate = o.OrderDate,
                                 ShipCity = o.ShipCity
                             };

                return result.FirstOrDefault();
            }
        }

        public List<OrderDetailDto> GetAllOrderDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var result = from o in context.Orders
                             join e in context.Employees on o.EmployeeId equals e.EmployeeId
                             join c in context.Customers on o.CustomerId equals c.CustomerId
                             select new OrderDetailDto
                             {
                                 OrderId = o.OrderId,
                                 CompanyName = c.CompanyName,
                                 EmployeeName = $"{e.FirstName} {e.LastName}",
                                 OrderDate = o.OrderDate,
                                 ShipCity = o.ShipCity
                             };
                return result.ToList();
            }
        }
    }
}
