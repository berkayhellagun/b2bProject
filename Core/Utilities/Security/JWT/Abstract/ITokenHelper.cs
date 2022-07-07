using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Security.JWT.Abstract
{
    public interface ITokenHelper
    {
        Task<AccessToken> AsyncCreateToken(User user, List<OperationClaim> operationClaims);
    }
}
