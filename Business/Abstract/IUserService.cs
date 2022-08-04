using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService : IGenericService<User>
    {
        Task<IDataResult<User>> AsyncGetByMail(string email);
        IDataResult<List<OperationClaim>> GetClaim(User user);
    }
}
