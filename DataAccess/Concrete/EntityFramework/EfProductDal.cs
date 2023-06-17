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
            throw new NotImplementedException();
        }

        public List<ProductDetails> GetProductWithProperties()
        {
            throw new NotImplementedException();
        }
    }
}
