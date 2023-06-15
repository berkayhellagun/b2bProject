using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Entities.Relationships;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoOrderDal : NeoGenericRepositoryBase<Order>, IOrderDal
    {
        IGraphClient _graphClient;
        public NeoOrderDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public List<OrderDetail> GetOrderDetail()
        {
            var orders = _graphClient.Cypher
                .Match("(ord:Order)-[rOrd:ORDERED]->(prd1:Product)-[rProp:PROPERTY]->(prop:Property)")
                .Match("(prs1:Person)-[rSell:SELLING]->(prd1)")
                .Match("(prs2:Person)-[rBuy:BOUGHT]->(ord)")
                .Where((Order ord, ORDERED rOrd, Product prd1, PROPERTY rProp, Property prop,
                        Person prs1, SELLING rSell, Person prs2, BOUGHT rBuy) =>
                        ord.Id == rOrd.OrderId && prd1.Id == rOrd.ProductId
                        && prs1.Id == rSell.PersonId && prd1.Id == rSell.ProductId
                        && prs2.Id == rBuy.PersonId && ord.Id == rBuy.OrderId
                        && prd1.Id == rProp.ProductId
                        )
                .Return((ord, prs1, prs2, prd1, prop) => new OrderDetail
                {
                    Id = ord.As<Order>().Id,
                    Order = ord.As<Order>(),
                    Seller = prs1.As<Person>(),
                    Buyer = prs2.As<Person>(),
                    Product = prd1.As<Product>(),
                    Properties = prop.CollectAs<Property>()
                })
                .ResultsAsync.Result.ToList();

            return orders;
        }

        public List<OrderDetail> GetOrderDetailById(int orderId)
        {
            var orders = _graphClient.Cypher
                .Match("(ord:Order)-[rOrd:ORDERED]->(prd1:Product)-[rProp:PROPERTY]->(prop:Property)")
                .Match("(prs1:Person)-[rSell:SELLING]->(prd1)")
                .Match("(prs2:Person)-[rBuy:BOUGHT]->(ord)")
                .Where((Order ord, ORDERED rOrd, Product prd1, PROPERTY rProp, Property prop,
                        Person prs1, SELLING rSell, Person prs2, BOUGHT rBuy) =>
                        ord.Id == rOrd.OrderId && prd1.Id == rOrd.ProductId
                        && prs1.Id == rSell.PersonId && prd1.Id == rSell.ProductId
                        && prs2.Id == rBuy.PersonId && ord.Id == rBuy.OrderId
                        && prd1.Id == rProp.ProductId
                        && ord.Id == orderId
                        )
                .Return((ord, prs1, prs2, prd1, prop) => new OrderDetail
                {
                    Id = ord.As<Order>().Id,
                    Order = ord.As<Order>(),
                    Seller = prs1.As<Person>(),
                    Buyer = prs2.As<Person>(),
                    Product = prd1.As<Product>(),
                    Properties = prop.CollectAs<Property>()
                })
                .ResultsAsync.Result.ToList();

            return orders;
        }
    }
}
