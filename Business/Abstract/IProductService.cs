﻿using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService : IGenericService<Product>
    {
        IDataResult<List<ProductDetail>> GetProductDetail(int id);
        IDataResult<List<Product>> GetProductsBySubCategoryId(int subCatId);
        IDataResult<List<Product>> GetProductsByCategoryId(int categoryId);
        IDataResult<List<Product>> GetBySupplierId(int supplierId);
        IDataResult<List<ProductDetails>> GetProductsDetails();
        IDataResult<ProductDetails> GetProductDetailsById(int productId);

    }
}
