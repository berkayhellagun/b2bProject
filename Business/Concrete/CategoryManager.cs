using Business.Abstract;
using Business.BusinessAspect;
using Business.Constants;
using Business.Validation.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.NLog.Types;
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
    // I used Facade Design Patterns on Business Class Library.
    // Business classes never know that what happened at the low classes.
    // In this manner i broke the dependency problem.
    public class CategoryManager : ICategoryService
    {
        readonly ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        [SecuredOperation("admin,")]
        [LogAspect(typeof(FileLogger))]
        [ValidationAspect(typeof(CategoryValidator))]
        public async Task<IResult> AsyncAdd(Category t)
        {
            var result = await _categoryDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }
        [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public async Task<IDataResult<List<Category>>> AsyncGetAll()
        {
            var result = await _categoryDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<Category>>(result)
                : new ErrorDataResult<List<Category>>(Messages.Error);
        }
        [CacheAspect]
        public async Task<IDataResult<Category>> AsyncGetById(int id)
        {
            var result = await _categoryDal.AsyncGetDB(c => c.Id == id);
            return result != null
                ? new SuccessDataResult<Category>(result)
                : new ErrorDataResult<Category>(Messages.Error);
        }

        public async Task<IResult> AsyncRemove(Category t)
        {
            var result = await _categoryDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        public async Task<IResult> AsyncUpdate(Category t)
        {
            var result = await _categoryDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        public async Task<IResult> RemoveById(int id)
        {
            var category = await AsyncGetById(id);
            var result = await AsyncRemove(category.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }
    }
}
