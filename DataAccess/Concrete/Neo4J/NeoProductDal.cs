using Core.DataAccess.Concrete.EntityFramework;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Relationships;
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

        public List<Product> GetProductsBySubCategoryId(int subCategoryId)
        {
            var products = _graphClient.Cypher
                .Match("p=(pp)-[]-(s:Product)-[i:INNER]->()")
                //.Match("p=(s:Product)-[i:INNER]->()")
                .Where((INNER i, Property pp) => i.SubCatId == subCategoryId && (pp.Key == "Brand" || pp.Key == "Model"))
                .Return((s, pp) => new Product
                //.Return((s) => new Product
                {
                    Id = s.As<Product>().Id,
                    ProductName = s.As<Product>().ProductName,
                    ProductCountry = s.As<Product>().ProductCountry,
                    ProductDescription = s.As<Product>().ProductDescription,
                    ProductInStock = s.As<Product>().ProductInStock,
                    ProductPrice = s.As<Product>().ProductPrice,
                    ProductionDate = s.As<Product>().ProductionDate,
                    ProductSalesAmount = s.As<Product>().ProductSalesAmount,
                    ProductSupplierId = s.As<Product>().ProductSupplierId,
                    ProductSubCategoryId = s.As<Product>().ProductSubCategoryId,
                    Properties = pp.CollectAs<Property>() //(IEnumerable<Property>)pp.CollectAs<Property>().Select(s=> s.Key == "Brand" && s.Key == "Model"),
                })
                .OrderBy("1")
                .ResultsAsync.Result.ToList();

            return products;
        }

        public List<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _graphClient.Cypher
                //.Match("p=(pp)-[]-(s:Product)-[i:INNER]->()")
                .Match("p=(s:Product)-[INNER]->()-[i:INNER]->()")
                .Where((INNER i) => i.CategoryId == categoryId)
                //.Return((s, pp) => new Product
                .Return((s) => new Product
                {
                    Id = s.As<Product>().Id,
                    ProductName = s.As<Product>().ProductName,
                    ProductCountry = s.As<Product>().ProductCountry,
                    ProductDescription = s.As<Product>().ProductDescription,
                    ProductInStock = s.As<Product>().ProductInStock,
                    ProductPrice = s.As<Product>().ProductPrice,
                    ProductionDate = s.As<Product>().ProductionDate,
                    ProductSalesAmount = s.As<Product>().ProductSalesAmount,
                    ProductSupplierId = s.As<Product>().ProductSupplierId,
                    ProductSubCategoryId = s.As<Product>().ProductSubCategoryId,
                    //Properties = pp.CollectAs<Property>()
                })
                .OrderBy("1")
                .ResultsAsync.Result.ToList();

            return products;
        }

        public List<ProductDetail> GetProductDetail(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetails> GetProductsDetails()
        {
            var products = _graphClient.Cypher
                .Match("(c:Category)<-[:INNER]-(s:SubCategory)<-[:INNER]-(p:Product)-[:PROPERTY]->(a:Property)")
                .Return((c, s, p, a) => new ProductDetails
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
