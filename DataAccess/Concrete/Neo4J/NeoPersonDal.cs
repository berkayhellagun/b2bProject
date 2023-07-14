using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Relationships;
using Neo4j.Driver;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoPersonDal : NeoGenericRepositoryBase<Person>, IPersonDal
    {
        private readonly IGraphClient _graphClient;

        public NeoPersonDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public bool AuthPerson(string mail, string pwd)
        {
            var auth = _graphClient.Cypher
                .Match("(p:Person)")
                .Where((Person p) => p.eMail == mail && p.Password == pwd && p.Status == 1)
                .Return((p) => new Person
                {
                    Id = p.As<Person>().Id,
                    FirstName = p.As<Person>().FirstName,
                    LastName = p.As<Person>().LastName,
                    CompanyName = p.As<Person>().CompanyName,
                    NickName = p.As<Person>().NickName,
                    eMail = p.As<Person>().eMail,
                    Password = p.As<Person>().Password,
                    isCanSell = p.As<Person>().isCanSell,
                    PersonType = p.As<Person>().PersonType,
                    Status = p.As<Person>().Status,
                    TCVKN = p.As<Person>().TCVKN
                }).ResultsAsync.Result.ToList();
            
            return auth.Capacity > 0 ? true : false;
        }

        public List<OperationClaim> GetClaims(Person user)
        {
            var claims = _graphClient.Cypher
                .Match("(u:User)-[:USER_OPERATION_CLAIM]->(oc:OperationClaim)")
                .Where((Person u) => u.Id == user.Id)
                .Return((oc) => new OperationClaim
                {
                    Id = oc.As<OperationClaim>().Id,
                    OperationName = oc.As<OperationClaim>().OperationName
                })
                .OrderBy("1")
                .ResultsAsync.Result.ToList();

            return claims;

        }

        public bool connectPersonToOrder(int orderId, int personId)
        {
            try
            {
                var lastInner = _graphClient.Cypher
                .Match("()-[c:BOUGHT]-()")
                .Return((c) => new BOUGHT
                {
                    Id = c.As<BOUGHT>().Id
                }).OrderByDescending("c.Id").Limit(1).ResultsAsync.Result.ToList();

                int lastId = lastInner.FirstOrDefault().Id + 1;

                _graphClient.Cypher.Match("(o:Order), (p:Person)")
                    .Where((Order o, Person p) => o.Id == orderId && p.Id == personId)
                    .Create("(p)-[:ORDERED{Id:" + lastId + ", PersonId:" + personId + ", OrderId:" + orderId + "}]->(o)")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<ProductDetails> GetProductsBySellerId(int sellerId)
        {

            var products = _graphClient.Cypher
                .Match("(c:Category)<-[:INNER]-(s:SubCategory)<-[:INNER]-(p:Product)-[:PROPERTY]->(a:Property)")
                .Match("(p)<-[:SELLING]-(pp:Person)")
                .Return((c, s, p, a, pp) => new ProductDetails
                {
                    Id = p.As<Product>().Id,
                    CategoryName = c.As<Category>().CategoryName,
                    SubCategoryName = s.As<SubCategory>().SubCategoryName,
                    ProductName = p.As<Product>().ProductName,
                    ProductDescription = p.As<Product>().ProductDescription,
                    ProductPrice = p.As<Product>().ProductPrice,
                    ProductInStock = p.As<Product>().ProductInStock,
                    ProductionDate = p.As<Product>().ProductionDate,
                    SellerId = pp.As<Person>().Id,
                    SellerNickName = pp.As<Person>().NickName,
                    ProductCountry = p.As<Product>().ProductCountry,
                    Properties = a.CollectAs<Property>()
                })
                .OrderBy("Id")
                .ResultsAsync.Result.ToList();

            return products;

        }

        public List<Person> GetSellerByCategoryId(int categoryId)
        {
            var seller = _graphClient.Cypher
                .Match("(p:Person)-[:CONCERNED]->(s:Sector)<-[:INNER]-(c:Category)")
                .Where((Category c) => c.Id == categoryId)
                .Return((p) => p.As<Person>()).ResultsAsync.Result.ToList();

            return seller;
        }
        /*
match a=(p:Person)-[:CONCERNED]->(s:Sector)<-[:INNER]-(c:Category)
where c.Id = 5
return a
*/
    }
}
