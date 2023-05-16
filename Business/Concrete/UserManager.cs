using Business.Abstract;
using Business.BusinessAspect;
using Business.Constants;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Caching;
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
        readonly ISupplierService _supplierService;

        public UserManager(IUserDal userDal, ISupplierService supplierService)
        {
            _userDal = userDal;
            _supplierService = supplierService;
        }
        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> AsyncAdd(User t)
        {
            if(t.SupplierId != null)
            {
                var supplierName = BindSupplierName(t.SupplierId.Value);
                t.FirstName = supplierName;
            }
            var result = await _userDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }
        [SecuredOperation(Roles ="admin,user.add")]
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

        public IDataResult<List<OperationClaim>> GetClaim(User user)
        {
            var result = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(result);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> AsyncRemove(User t)
        {
            var result = await _userDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> AsyncUpdate(User t)
        {
            var result = await _userDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        private string BindSupplierName(int supplierId)
        {
            var supplier = _supplierService.AsyncGetById(supplierId).Result;
            if (supplier.Data == null)
            {
                return string.Empty;
            }
            return supplier.Data.SupplierName;
        }

        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> RemoveById(int id)
        {
            var user = await AsyncGetById(id);
            var result = await AsyncRemove(user.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }
    }
}
