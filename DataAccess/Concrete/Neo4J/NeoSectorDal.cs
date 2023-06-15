using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoSectorDal : NeoGenericRepositoryBase<Sector>, ISectorDal
    {
        private IGraphClient _graphClient;
        public NeoSectorDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public List<Person> GetFirmGroupBySector()
        {
            var firms = _graphClient.Cypher
                .Match("(f:Person)-[:CONCERNED]->(:Sector)")
                .Where((Person f) => f.PersonType == 1 && f.isCanSell == true)
                .Return((f) => f.As<Person>())
                .ResultsAsync.Result.ToList();

            return firms;
        }

        public List<Person> GetFirmGroupBySectorBySectorId(int sectorId)
        {
            var firms = _graphClient.Cypher
                .Match("(f:Person)-[:CONCERNED]->(s:Sector)")
                .Where((Person f, Sector s) => f.PersonType == 1 && f.isCanSell == true && s.Id == sectorId)
                .Return((f) => f.As<Person>())
                .ResultsAsync.Result.ToList();

            return firms;
        }
    }
}
