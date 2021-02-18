using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
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
        [ValidationAspect(typeof(OrderValidator))]
        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult(Messages.Orders.Add());
        }
        public IResult Delete(Order order)
        {
            var result = _orderDal.Get(o => o.OrderId == order.OrderId);
            if (result != null)
            {
                _orderDal.Delete(order);
                return new SuccessResult(Messages.Orders.Delete());
            }
            return new ErrorResult(Messages.NotFound);
        }
        [ValidationAspect(typeof(OrderValidator))]
        public IResult Update(Order order)
        {
            var result = _orderDal.Get(o => o.OrderId == order.OrderId);
            if (result != null)
            {
                _orderDal.Update(order);
                return new SuccessResult(Messages.Orders.Update());
            }
            return new ErrorResult(Messages.NotFound);
        }
        public IDataResult<Order> GetById(int orderId)
        {
            var result = _orderDal.Get(o => o.OrderId == orderId);
            if (result != null)
            {
                return new SuccessDataResult<Order>(result);
            }
            return new ErrorDataResult<Order>(Messages.NotFound);
        }

        public IDataResult<List<Order>> GetAll()
        {
            var result = _orderDal.GetAll();
            if (result != null)
            {
                return new SuccessDataResult<List<Order>>(result);
            }
            return new ErrorDataResult<List<Order>>(Messages.NotFound);
        }

        public IDataResult<List<Order>> GetAllByCustomerId(string customerId)
        {
            var result = _orderDal.GetAll(o => o.CustomerId == customerId);
            if (result != null)
            {
                return new SuccessDataResult<List<Order>>(result);
            }
            return new ErrorDataResult<List<Order>>(Messages.NotFound);
        }

        public IDataResult<List<Order>> GetAllByEmployeeId(int employeeId)
        {
            var result = _orderDal.GetAll(o => o.EmployeeId == employeeId);
            if (result != null)
            {
                return new SuccessDataResult<List<Order>>(result);
            }
            return new ErrorDataResult<List<Order>>(Messages.NotFound);
        }

        public IDataResult<List<OrderDetailDto>> GetAllOrderDetails()
        {
            var result = _orderDal.GetAllOrderDetails();
            if (result != null)
            {
                return new SuccessDataResult<List<OrderDetailDto>>(result);
            }
            return new ErrorDataResult<List<OrderDetailDto>>(Messages.NotFound);
        }

        public IDataResult<OrderDetailDto> GetOrderDetail(int orderId)
        {
            var result = _orderDal.Get(o => o.OrderId == orderId);
            if (result != null)
            {
                return new SuccessDataResult<OrderDetailDto>(_orderDal.GetOrderDetail(orderId));
            }
            return new ErrorDataResult<OrderDetailDto>(Messages.NotFound);
        }

    }
}
