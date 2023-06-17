using Core.DataAccess.Abstract;
using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepositoryBase<Product>
    {
        List<ProductDetail> GetProductDetail(int id);
        List<ProductDetails> GetProductsDetails();
        List<Product> GetProductsBySubCategoryId(int subCatId);
        List<Product> GetProductsByCategoryId(int categoryId);
        List<Product> GetProductsBySectorId(int sectorId);
        List<Tuple<Product,int>> GetProductsMostOrderedLast24Hours();
        bool connectSubCategory(int subId, int productId);
        bool connectOrder(int orderId, int productId);
        bool connectProperty(int propertyId, int productId);
        bool connectSeller(int sellerId, int productId);

    }
}
