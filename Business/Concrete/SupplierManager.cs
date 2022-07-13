using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
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

        public SupplierManager(ISupplierDal supplierDal)
        {
            _supplierDal = supplierDal;
        }

        public async Task<IResult> AsyncAdd(Supplier t)
        {
            var result = await _supplierDal.AsyncAddDB(t);
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
                ? new SuccessDataResult<Supplier>()
                : new ErrorDataResult<Supplier>(Messages.Error);
        }

        public async Task<IResult> AsyncRemove(Supplier t)
        {
            var result = await _supplierDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        public async Task<IResult> AsyncUpdate(Supplier t)
        {
            var result = await _supplierDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }
    }
}
