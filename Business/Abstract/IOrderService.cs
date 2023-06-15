using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService : IGenericService<Order>
    {
        IDataResult<List<OrderDetail>> GetOrderDetail();
        IDataResult<List<OrderDetail>> GetOrderDetailById(int orderId);
    }
}
