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
    public class PersonManager : IPersonService
    {
        readonly IPersonDal _userDal;
        readonly ISupplierService _supplierService;

        public PersonManager(IPersonDal userDal, ISupplierService supplierService)
        {
            _userDal = userDal;
            _supplierService = supplierService;
        }
        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> AsyncAdd(Person t)
        {
            if(t.CompanyName != null)
            {
                //var supplierName = BindSupplierName(t.SupplierId.Value);

                var supplierName = t.CompanyName;
                t.FirstName = supplierName;
            }
            var result = await _userDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }
        [SecuredOperation(Roles ="admin,user.add")]
        [CacheAspect]
        public async Task<IDataResult<List<Person>>> AsyncGetAll()
        {
            var result = await _userDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<Person>>(result)
                : new ErrorDataResult<List<Person>>(Messages.Error);
        }
        [CacheAspect]
        public async Task<IDataResult<Person>> AsyncGetById(int id)
        {
            var result = await _userDal.AsyncGetDB(u => u.Id == id);
            return result != null
                ? new SuccessDataResult<Person>(result)
                : new ErrorDataResult<Person>(Messages.Error);
        }

        public async Task<IDataResult<Person>> AsyncGetByMail(string email)
        {
            var result = await _userDal.AsyncGetDB(u => u.eMail == email);
            return result != null
                ? new SuccessDataResult<Person>(result)
                : new ErrorDataResult<Person>();
        }

        public IDataResult<List<OperationClaim>> GetClaim(Person user)
        {
            var result = _userDal.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(result);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> AsyncRemove(Person t)
        {
            var result = await _userDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> AsyncUpdate(Person t)
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
