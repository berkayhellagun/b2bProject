using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.IdentityModel.Tokens;
using Neo4j.Driver;
using Neo4jClient;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoProductDal : NeoGenericRepositoryBase<Product>, IProductDal
    {
        private readonly IGraphClient _graphClient;

        public NeoProductDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public List<ProductDetail> GetProductDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductWithProperties> GetProductWithProperties()
        {
            var products = _graphClient.Cypher
                .Match("(c:Category)<-[:INNER]-(s:SubCategory)<-[:INNER]-(p:Product)-[:PROPERTY]->(a:Property)")
                .Return((c, s, p, a) => new ProductWithProperties
                {
                    Id = p.As<Product>().Id,
                    CategoryName = c.As<Category>().CategoryName,
                    SubCategoryName = s.As<SubCategory>().SubCategoryName,
                    ProductName = p.As<Product>().ProductName,
                    ProductDescription = p.As<Product>().ProductDescription,
                    ProductPrice = p.As<Product>().ProductPrice,
                    ProductInStock = p.As<Product>().ProductInStock,
                    Properties = a.CollectAs<Property>()
                })
                .OrderBy("Id")
                .ResultsAsync.Result.ToList();

            return products;
        }
    }
}
