using Core.DataAccess.Concrete.EntityFramework;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Relationships;
using Microsoft.IdentityModel.Tokens;
using Neo4j.Driver;
using Neo4jClient;
using System;

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
                .Match("p=(pp)-[]-(s:Product)-[i:INNER]->(:SubCategory)")
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
                .Match("p=(s:Product)-[:INNER]->(:SubCategory)-[i:INNER]->(:Category)")
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

        public List<Product> GetProductsBySectorId(int sectorId)
        {
            var products = _graphClient.Cypher
                .Match("(s:Product)-[:INNER]->(:SubCategory)-[:INNER]->(:Category)-[i:INNER]->(:Sector)")
                .Where((INNER i) => i.SectorId == sectorId)
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
                })
                .OrderBy("1")
                .ResultsAsync.Result.ToList();

            return products;
        }

        public List<ProductDetail> GetProductDetail(int id)
        {
            return null;
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

        public bool connectSubCategory(int subId, int productId)
        {
            try
            {
                var lastInner = _graphClient.Cypher
                .Match("()-[c:INNER]-()")
                .Return((c) => new INNER
                {
                    Id = c.As<INNER>().Id
                }).OrderByDescending("c.Id").Limit(1).ResultsAsync.Result.ToList();

                int lastId = lastInner.FirstOrDefault().Id + 1;

                _graphClient.Cypher.Match("(s:SubCategory), (p:Product)")
                    .Where((SubCategory s, Product p) => s.Id == subId && p.Id == productId)
                    .Create("(p)-[:INNER{Id:" + lastId + ", ProductId:" + productId + ", SubCatId:" + subId + "}]->(s)")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool connectOrder(int orderId, int productId)
        {
            try
            {
                var lastInner = _graphClient.Cypher
                .Match("()-[c:ORDERED]-()")
                .Return((c) => new ORDERED
                {
                    Id = c.As<ORDERED>().Id
                }).OrderByDescending("c.Id").Limit(1).ResultsAsync.Result.ToList();

                int lastId = lastInner.FirstOrDefault().Id + 1;

                _graphClient.Cypher.Match("(o:Order), (p:Product)")
                    .Where((Order o, Product p) => o.Id == orderId && p.Id == productId)
                    .Create("(p)-[:ORDERED{Id:" + lastId + ", OrderId:" + orderId + ", ProductId:" + productId + "}]->(p)")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool connectProperty(int propertyId, int productId)
        {
            try
            {
                var lastInner = _graphClient.Cypher
                .Match("()-[c:PROPERTY]-()")
                .Return((c) => new PROPERTY
                {
                    Id = c.As<PROPERTY>().Id
                }).OrderByDescending("c.Id").Limit(1).ResultsAsync.Result.ToList();

                int lastId = lastInner.FirstOrDefault().Id + 1;

                _graphClient.Cypher.Match("(pp:Property), (p:Product)")
                    .Where((Property pp, Product p) => pp.Id == propertyId && p.Id == productId)
                    .Create("(p)-[:PROPERTY{Id:" + lastId + ", PropertyId:" + propertyId + ", ProductId:" + productId + "}]->(pp)")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /*
public bool connectSeller(int sellerId, int productId)
{
   try
   {
       var lastInner = _graphClient.Cypher
       .Match("()-[c:INNER]-()")
       .Return((c) => new SELLING
       {
           Id = c.As<SELLING>().Id
       }).OrderByDescending("c.Id").Limit(1).ResultsAsync.Result.ToList();

       int lastId = lastInner.FirstOrDefault().Id + 1;

       _graphClient.Cypher.Match("(o:Order), (p:Product)")
           .Where((Person prs, Product p) => o.Id == sellerId && p.Id == productId)
           .Create("(prs)-[:SELLING{Id:" + lastId + ", PersonId:" + sellerId + ", ProductId:" + productId + "}]->(p)")
           .ExecuteWithoutResultsAsync();
       return true;
   }
   catch (Exception)
   {
       return false;
   }
}
*/
    }
}
