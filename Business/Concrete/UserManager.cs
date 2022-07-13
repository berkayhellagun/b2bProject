using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<IResult> AsyncAdd(User t)
        {
            var result = await _userDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }
        [CacheAspect]
        public async Task<IDataResult<List<User>>> AsyncGetAll()
        {
            var result = await _userDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<User>>(result)
                : new ErrorDataResult<List<User>>(Messages.Error);
        }
        [CacheAspect]
        public async Task<IDataResult<User>> AsyncGetById(int id)
        {
            var result = await _userDal.AsyncGetDB(u => u.UserId == id);
            return result != null
                ? new SuccessDataResult<User>(result)
                : new ErrorDataResult<User>(Messages.Error);
        }

        public async Task<IDataResult<User>> AsyncGetByMail(string email)
        {
            var result = await _userDal.AsyncGetDB(u => u.Email == email);
            return result != null
                ? new SuccessDataResult<User>(result)
                : new ErrorDataResult<User>();
        }

        public async Task<List<OperationClaim>> AsyncGetClaim(User user)
        {
            var result = await _userDal.AsyncGetClaimsDB(user);
            return result != null ? result : null;
        }

        public async Task<IResult> AsyncRemove(User t)
        {
            var result = await _userDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        public async Task<IResult> AsyncUpdate(User t)
        {
            var result = await _userDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }
    }
}
