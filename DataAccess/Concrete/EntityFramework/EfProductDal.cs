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
    public class EfProductDal : EfEntityRepositoryBase<Product, DBContext>, IProductDal
    {
        public List<ProductDetail> GetProductDetail(int id)
        {
            using (DBContext db = new DBContext())
            {
                var result = from p in db.Products
                             join c in db.Categories on p.ProductCategoryId equals c.Id
                             join s in db.Suppliers on p.ProductSupplierId equals s.SupplierId
                             where p.ProductId == id
                             select new ProductDetail
                             {
                                 Name = p.ProductName,
                                 Price = p.ProductPrice,
                                 SupplierName = s.SupplierName,
                                 CategoryName = c.Name,
                                 Country = p.ProductCountry,
                                 Description = p.ProductDescription,
                                 Date = p.ProductionDate,
                                 SupplierId = s.SupplierId,
                                 InStock = p.ProductInStock,
                                 ProductImage = p.ProductImage,
                                 CategoryId = c.Id
                             };
                return result.ToList();
            }
        }
    }
}
