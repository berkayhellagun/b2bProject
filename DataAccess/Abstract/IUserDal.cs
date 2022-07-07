﻿using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepositoryBase<User>
    {
        Task<List<OperationClaim>> AsyncGetClaimsDB(User user);
    }
}
