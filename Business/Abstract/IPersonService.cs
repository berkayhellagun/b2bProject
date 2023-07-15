﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IPersonService : IGenericService<Person>
    {
        Task<IDataResult<Person>> AsyncGetByMail(string email);
        IDataResult<List<OperationClaim>> GetClaim(Person user);
        IDataResult<Person> AuthPerson(string mail, string pwd);
        Task<IResult> connectPersonToOrder(int orderId, int personId);
        IDataResult<List<ProductDetails>> GetProductsBySellerId(int sellerId);
        IDataResult<List<Person>> GetSellerByCategoryId(int categoryId);
    }
}
