using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
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
    public class SubCategoryManager: ISubCategoryService
    {
        ISubCategoryDal _subCategoryDal;
        public SubCategoryManager(ISubCategoryDal subCategoryDal)
        {
            _subCategoryDal = subCategoryDal;
        }

        [CacheRemoveAspect("ISubCategoryService.Get")]
        public async Task<IResult> AsyncAdd(SubCategory t)
        {
            var result = await _subCategoryDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }

        [CacheAspect]
        public async Task<IDataResult<List<SubCategory>>> AsyncGetAll()
        {
            var result = await _subCategoryDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<SubCategory>>(result)
                : new ErrorDataResult<List<SubCategory>>(Messages.Error);
        }

        public async Task<IDataResult<SubCategory>> AsyncGetById(int id)
        {
            var result = await _subCategoryDal.AsyncGetDB(p => p.Id == id);
            return result != null
                ? new SuccessDataResult<SubCategory>(result)
                : new ErrorDataResult<SubCategory>(Messages.Error);
        }

        [CacheRemoveAspect("ISubCategoryService.Get")]
        [TransactionAspect]
        public async Task<IResult> AsyncRemove(SubCategory t)
        {
            var result = await _subCategoryDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        [CacheRemoveAspect("ISubCategoryService.Get")]
        public async Task<IResult> AsyncUpdate(SubCategory t)
        {
            var result = await _subCategoryDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        [CacheRemoveAspect("ISubCategoryService.Get")]
        public async Task<IResult> RemoveById(int id)
        {
            var subCategoryDal = await AsyncGetById(id);
            var result = await AsyncRemove(subCategoryDal.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }
    }
}
