using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
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
    }
}
