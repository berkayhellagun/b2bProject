using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Neo4jClient;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoProductDal : NeoGenericRepositoryBase<Product>, IProductDal
    {
        public NeoProductDal(IGraphClient graphClient) : base(graphClient)
        {
        }

        public List<ProductDetail> GetProductDetail(int id)
        {
            throw new NotImplementedException();
        }
    }
}
