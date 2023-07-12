using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IPersonDal : IEntityRepositoryBase<Person>
    {
        List<OperationClaim> GetClaims(Person user);
        bool AuthPerson(string mail, string pwd);
        bool connectPersonToOrder(int orderId, int personId);
        List<ProductDetails> GetProductsBySellerId(int sellerId);
        List<Person> GetSellerByCategoryId(int categoryId);
    }
}
