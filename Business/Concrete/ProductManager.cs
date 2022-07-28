using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task<IResult> AsyncAdd(Product t)
        {
            var result = await _productDal.AsyncAddDB(t);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }
        [CacheAspect]
        public async Task<IDataResult<List<Product>>> AsyncGetAll()
        {
            var result = await _productDal.AsyncGetAllDB();
            return result != null
                ? new SuccessDataResult<List<Product>>(result)
                : new ErrorDataResult<List<Product>>(Messages.Error);
        }

        public IDataResult<List<ProductDetail>> GetProductDetail()
        {
            return new SuccessDataResult<List<ProductDetail>>(_productDal.GetProductDetail());
        }


        [CacheAspect]
        public async Task<IDataResult<Product>> AsyncGetById(int id)
        {
            var result = await _productDal.AsyncGetDB(p => p.ProductId == id);
            return result != null
                ? new SuccessDataResult<Product>(result)
                : new ErrorDataResult<Product>(Messages.Error);
        }

        public async Task<IResult> AsyncRemove(Product t)
        {
            var result = await _productDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        public async Task<IResult> AsyncUpdate(Product t)
        {
            var result = await _productDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        public IDataResult<List<Product>> GetByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.ProductCategoryId == categoryId).ToList());
        }

        public IDataResult<List<Product>> GetBySupplierId(int supplierId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.ProductSupplierId == supplierId).ToList());
        }
    }
}
