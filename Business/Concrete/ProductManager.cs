using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.CrossCuttingConcerns.Caching;
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

        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> AsyncAdd(Product t)
        {
            t.ProductionDate = DateTime.Now;
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

        public IDataResult<List<ProductDetail>> GetProductDetail(int id)
        {
            return new SuccessDataResult<List<ProductDetail>>(_productDal.GetProductDetail(id));
        }


        [CacheAspect]
        public async Task<IDataResult<Product>> AsyncGetById(int id)
        {
            var result = await _productDal.AsyncGetDB(p => p.Id == id);
            return result != null
                ? new SuccessDataResult<Product>(result)
                : new ErrorDataResult<Product>(Messages.Error);
        }

        [CacheRemoveAspect("IProductService.Get")]
        [TransactionAspect]
        public async Task<IResult> AsyncRemove(Product t)
        {
            var result = await _productDal.AsyncDeleteDB(t);
            return result
                ? new SuccessResult(Messages.Removed)
                : new ErrorResult(Messages.NotRemoved);
        }

        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> AsyncUpdate(Product t)
        {
            var result = await _productDal.AsyncUpdateDB(t);
            return result
                ? new SuccessResult(Messages.Updated)
                : new ErrorResult(Messages.NotUpdated);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetProductsBySectorId(int sectorId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetProductsBySectorId(sectorId).ToList());
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetProductsByCategoryId(categoryId).ToList());
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetProductsBySubCategoryId(int subCatId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetProductsBySubCategoryId(subCatId).ToList());
        }

        public IDataResult<List<Product>> GetBySupplierId(int supplierId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.ProductSupplierId == supplierId).ToList());
        }

        [CacheRemoveAspect("IProductService.Get")]
        public async Task<IResult> RemoveById(int id)
        {
            var product = await AsyncGetById(id);
            var result = await AsyncRemove(product.Data);
            return result.Success
                ? new SuccessResult()
                : new ErrorResult();
        }

        [CacheAspect]
        public IDataResult<List<ProductDetails>> GetProductsDetails()
        {
            return new SuccessDataResult<List<ProductDetails>>(_productDal.GetProductsDetails().ToList());
        }

        public IDataResult<ProductDetails> GetProductDetailsById(int productId)
        {
            return new SuccessDataResult<ProductDetails>(_productDal.GetProductsDetails().Where(x=> x.Id == productId).First());
        }

        [CacheAspect]
        public async Task<IResult> connectSubCategory(int subId, int productId)
        {
            var result = _productDal.connectSubCategory(subId, productId);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }

        [CacheAspect]
        public async Task<IResult> connectOrder(int orderId, int productId)
        {
            var result = _productDal.connectOrder(orderId, productId);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }

        [CacheAspect]
        public async Task<IResult> connectProperty(int propertyId, int productId)
        {
            var result = _productDal.connectProperty(propertyId, productId);
            return result
                ? new SuccessResult(Messages.Added)
                : new ErrorResult(Messages.NotAdded);
        }
    }
}
