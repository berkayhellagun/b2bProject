using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, DBContext>//, IProductDal
    {
        public List<Product> GetBySubCategoryId(int subCatId)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetail> GetProductDetail(int id)
        {
            using (DBContext db = new DBContext())
            {
                var result = from p in db.Products
                             where p.Id == id
                             join c in db.SubCategories on p.ProductSubCategoryId equals c.Id
                             join s in db.Suppliers on p.ProductSupplierId equals s.Id

                             select new ProductDetail
                             {
                                 ProductName = p.ProductName,
                                 ProductPrice = p.ProductPrice,
                                 SupplierName = s.SupplierName,
                                 SubCategoryName = c.SubCategoryName,
                                 ProductCountry = p.ProductCountry,
                                 ProductDescription = p.ProductDescription,
                                 ProductionDate = p.ProductionDate,
                                 ProductSupplierId = s.Id,
                                 ProductInStock = p.ProductInStock,
                                 ProductSubCategoryId = c.Id,
                                 ProductSalesAmount = p.ProductSalesAmount
                             };
                return result.ToList();
            }
        }

        public List<ProductDetails> GetProductWithProperties()
        {
            throw new NotImplementedException();
        }
    }
}
