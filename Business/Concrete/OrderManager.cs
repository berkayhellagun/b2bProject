using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Relationships;
using Neo4jClient.Cypher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        [CacheRemoveAspect("IOrderService.Get")]
        public async Task<IResult> AsyncAdd(Order t)
        {
            t.OrderDate = DateTime.Now;
            var result = await _orderDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }

        [CacheAspect]
        public async Task<IDataResult<List<Order>>> AsyncGetAll()
        {
            var result = await _orderDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<Order>>(result)
                : new ErrorDataResult<List<Order>>(Messages.Error);
        }

        public async Task<IDataResult<Order>> AsyncGetById(int id)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>
            {
                { "Id", id },
            };
            var result = await _orderDal.AsyncGetDB(filter);
            return result != null
                ? new SuccessDataResult<Order>(result)
                : new ErrorDataResult<Order>(Messages.Error);
        }

        [CacheRemoveAspect("IOrderService.Get")]
        [TransactionAspect]
        public async Task<IResult> AsyncRemove(Order t)
        {
            var result = await _orderDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        [CacheRemoveAspect("IOrderService.Get")]
        public async Task<IResult> AsyncUpdate(Order t)
        {
            var result = await _orderDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        [CacheRemoveAspect("IOrderService.Get")]
        public async Task<IResult> RemoveById(int id)
        {
            var order = await AsyncGetById(id);
            var result = await AsyncRemove(order.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }

        [CacheAspect]
        public IDataResult<List<OrderDetail>> GetOrderDetail()
        {
            //return new SuccessDataResult<List<OrderDetail>>(_orderDal.GetOrderDetail());
            var result = _orderDal.GetOrderDetail();
            return result != null
                ? new SuccessDataResult<List<OrderDetail>>(result)
                : new ErrorDataResult<List<OrderDetail>>(Messages.Error);
            
            //return null;
        }

        public IDataResult<List<OrderDetail>> GetOrderDetailById(int orderId)
        {
            var result =  _orderDal.GetOrderDetailById(orderId);
            return result != null
                ? new SuccessDataResult<List<OrderDetail>>(result)
                : new ErrorDataResult<List<OrderDetail>>(Messages.Error);

        }
    }
}
