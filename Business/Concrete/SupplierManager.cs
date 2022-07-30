using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SupplierManager : ISupplierService
    {
        readonly ISupplierDal _supplierDal;
        ICacheService _cacheService;

        public SupplierManager(ISupplierDal supplierDal, ICacheService cacheService)
        {
            _supplierDal = supplierDal;
            _cacheService = cacheService;
        }

        public async Task<IResult> AsyncAdd(Supplier t)
        {
            var result = await _supplierDal.AsyncAddDB(t);
            _cacheService.Clear();
            return result
                ? new SuccessResult(message: Messages.Added)
                : new ErrorResult(message: Messages.NotAdded);
        }
        [CacheAspect]
        public async Task<IDataResult<List<Supplier>>> AsyncGetAll()
        {
            var result = await _supplierDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<Supplier>>(result)
                : new ErrorDataResult<List<Supplier>>(Messages.Error);
        }
        [CacheAspect]
        public async Task<IDataResult<Supplier>> AsyncGetById(int id)
        {
            var result = await _supplierDal.AsyncGetDB(s => s.SupplierId == id);
            return result != null
                ? new SuccessDataResult<Supplier>(result)
                : new ErrorDataResult<Supplier>(Messages.Error);
        }

        public async Task<IResult> AsyncRemove(Supplier t)
        {
            var result = await _supplierDal.AsyncDeleteDB(t);
            _cacheService.Clear();
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        public async Task<IResult> AsyncUpdate(Supplier t)
        {
            var result = await _supplierDal.AsyncUpdateDB(t);
            _cacheService.Clear();
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        public async Task<IResult> RemoveById(int id)
        {
            var supplier = await AsyncGetById(id);
            var result = await AsyncRemove(supplier.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }
    }
}
