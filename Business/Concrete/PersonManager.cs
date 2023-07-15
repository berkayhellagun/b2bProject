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
using DataAccess.Concrete.EntityFramework;
using Entities.DTOs;
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

        [CacheAspect]
        public async Task<IResult> connectPersonToOrder(int orderId, int personId)
        {
            var result = _userDal.connectPersonToOrder(orderId, personId);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }

        [ValidationAspect(typeof(UserValidator))]
        [CacheRemoveAspect("IUserService.Get")]
        public async Task<IResult> AsyncAdd(Person t)
        {
            if (t.CompanyName != null)
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
        [SecuredOperation(Roles = "admin,user.add")]
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
            var allData = await _userDal.AsyncGetAllDB();
            var result = allData.Where(x => x.Id == id).First();
            return result != null
                ? new SuccessDataResult<Person>(result)
                : new ErrorDataResult<Person>(Messages.Error);
        }

        public async Task<IDataResult<Person>> AsyncGetByMail(string email)
        {
            Dictionary<string, object> filter = new Dictionary<string, object>
            {
                { "eMail", email },
            };
            var allData = await _userDal.AsyncGetAllDB();
            var result = allData.Where(x => x.eMail == email).First();
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

        public IDataResult<Person> AuthPerson(string mail, string pwd)
        {
            var result = _userDal.AuthPerson(mail, pwd);
            return result != null
                ? new SuccessDataResult<Person>(result)
                : new ErrorDataResult<Person>();
        }

        public IDataResult<List<ProductDetails>> GetProductsBySellerId(int sellerId)
        {
            var result = _userDal.GetProductsBySellerId(sellerId).Where(x => x.SellerId == sellerId).ToList();
            return result != null
                ? new SuccessDataResult<List<ProductDetails>>(result)
                : new ErrorDataResult<List<ProductDetails>>();
        }

        public IDataResult<List<Person>> GetSellerByCategoryId(int sellerId)
        {
            var result = _userDal.GetSellerByCategoryId(sellerId);
            return result != null
                ? new SuccessDataResult<List<Person>>(result)
                : new ErrorDataResult<List<Person>>();
        }

    }
}
