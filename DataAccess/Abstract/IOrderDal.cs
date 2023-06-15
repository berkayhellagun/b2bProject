﻿using Core.DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepositoryBase<Order>
    {
        List<OrderDetail> GetOrderDetail();
        List<OrderDetail> GetOrderDetailById(int orderId);
    }
}
