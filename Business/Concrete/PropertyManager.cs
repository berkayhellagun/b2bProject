using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PropertyManager : IPropertyService
    {
        IPropertyDal _propertyDal;
        public PropertyManager(IPropertyDal propertyDal)
        {
            _propertyDal = propertyDal;
        }

        [CacheRemoveAspect("IPropertyService.Get")]
        public async Task<IResult> AsyncAdd(Property t)
        {
            var result = await _propertyDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }

        [CacheAspect]
        public async Task<IDataResult<List<Property>>> AsyncGetAll()
        {
            var result = await _propertyDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<Property>>(result)
                : new ErrorDataResult<List<Property>>(Messages.Error);
        }

        public async Task<IDataResult<Property>> AsyncGetById(int id)
        {
            var result = await _propertyDal.AsyncGetDB(p => p.Id == id);
            return result != null
                ? new SuccessDataResult<Property>(result)
                : new ErrorDataResult<Property>(Messages.Error);
        }

        [CacheRemoveAspect("IPropertyService.Get")]
        [TransactionAspect]
        public async Task<IResult> AsyncRemove(Property t)
        {
            var result = await _propertyDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        [CacheRemoveAspect("IPropertyService.Get")]
        public async Task<IResult> AsyncUpdate(Property t)
        {
            var result = await _propertyDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        [CacheRemoveAspect("IPropertyService.Get")]
        public async Task<IResult> RemoveById(int id)
        {
            var property = await AsyncGetById(id);
            var result = await AsyncRemove(property.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }
    }
}
