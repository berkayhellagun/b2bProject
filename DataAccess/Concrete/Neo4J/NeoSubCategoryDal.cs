using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Relationships;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoSubCategoryDal : NeoGenericRepositoryBase<SubCategory>, ISubCategoryDal
    {
        private IGraphClient _graphClient;
        public NeoSubCategoryDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public bool connectCategory(int subId, int catId)
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

                _graphClient.Cypher.Match("(s:SubCategory), (c:Category)")
                    .Where((SubCategory s, Category c) => s.Id == subId && c.Id == catId)
                    .Create("(s)-[:INNER{Id:" + lastId + ", CategoryId:" + catId + ", SubCatId:" + subId + "}]->(c)")
                    .ExecuteWithoutResultsAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }
    }
}
