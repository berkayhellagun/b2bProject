using Business.Abstract;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductImageManager : IImageService<ProductImage>
    {
        private readonly IProductImageDal _productImageDal;
        private readonly IFileHelper _fileHelper;
        private const string FilePath = "WebMVC/wwwroot/Images/ProductImage";

        public ProductImageManager(IProductImageDal productImageDal, IFileHelper fileHelper)
        {
            _productImageDal = productImageDal;
            _fileHelper = fileHelper;
        }

        public async Task<IDataResult<ProductImage>> GetAsync(int entityId)
        {
            return new SuccessDataResult<ProductImage>( await _productImageDal.AsyncGetDB(p => p.Id == entityId));
        }

        public async Task<Core.Utilities.Results.Abstract.IResult> AddAsync(IFormFile formFile, ProductImage entity)
        {
            entity.Path = _fileHelper.Upload(formFile, FilePath);
            await _productImageDal.AsyncAddDB(entity);
            return new SuccessResult();
        }

        public async Task<Core.Utilities.Results.Abstract.IResult> UpdateAsync(IFormFile formFile, ProductImage entity)
        {
           entity.Path = _fileHelper.Update(formFile, FilePath + entity.Path, FilePath);
            await _productImageDal.AsyncUpdateDB(entity);
            return new SuccessResult();
        }

        public async Task<Core.Utilities.Results.Abstract.IResult> DeleteAsync(IFormFile formFile, int entityId)
        {
            var imagePath = string.Format("/{0}.jpg", entityId);
            var fullPath = FilePath + imagePath;
            _fileHelper.Delete(fullPath);
            var image = GetAsync(entityId);
            await _productImageDal.AsyncDeleteDB(image.Result.Data);
            return new SuccessResult();
        }
    }
}
