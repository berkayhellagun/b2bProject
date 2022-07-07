using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DBContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new DBContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                                 on operationClaim.OperationId equals userOperationClaim.OperationId
                             where userOperationClaim.UserId == user.UserId
                             select new OperationClaim { OperationId= operationClaim.OperationId,
                                                        OperationName = operationClaim.OperationName };
                return result.ToList();

            }
        }
        public Task<List<OperationClaim>> AsyncGetClaimsDB(User user)
        {
            return Task.FromResult(GetClaims(user));
        }
    }
}
