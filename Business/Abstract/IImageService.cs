using Core.Entities;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IImageService<T> where T : class, IEntity, new()
    {
        Task<IDataResult<T>> GetAsync(int entityId);
        Task<IResult> AddAsync(Microsoft.AspNetCore.Http.IFormFile formFile, T entity);
        Task<IResult> UpdateAsync(Microsoft.AspNetCore.Http.IFormFile formFile, T entity);
        Task<IResult> DeleteAsync(Microsoft.AspNetCore.Http.IFormFile formFile, int entityId);
    }
}
