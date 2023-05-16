using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, DBContext>, IUserOperationClaimDal
    {
        public List<UserOperationClaimDetail> GetDetailByIdDB(int id)
        {
            using (var db = new DBContext())
            {
                var result = from userOperation in db.UserOperationClaims
                             join operationClaim in db.OperationClaims on userOperation.OperationId equals operationClaim.Id
                             join user in db.Users on userOperation.UserId equals user.Id
                             where userOperation.Id == id
                             select new UserOperationClaimDetail
                             {
                                 UserOperationId = userOperation.Id,
                                 OperationId = userOperation.OperationId,
                                 OperationName = operationClaim.OperationName,
                                 UserId = userOperation.UserId,
                                 UserName = user.FirstName + " " + user.LastName
                             };
                return result.ToList();
            }
        }

        public List<UserOperationClaimDetail> GetDetailDB()
        {
            using (var db = new DBContext())
            {
                var result = from userOperation in db.UserOperationClaims
                             join operationClaim in db.OperationClaims on userOperation.OperationId equals operationClaim.Id
                             join user in db.Users on userOperation.UserId equals user.Id
                             select new UserOperationClaimDetail
                             {
                                 UserOperationId = userOperation.Id,
                                 OperationId = userOperation.OperationId,
                                 OperationName = operationClaim.OperationName,
                                 UserId = userOperation.UserId,
                                 UserName = user.FirstName + " " + user.LastName
                             };
                return result.ToList();
            }
        }
    }
}
