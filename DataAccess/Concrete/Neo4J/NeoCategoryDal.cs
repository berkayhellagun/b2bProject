using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Neo4jClient;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoCategoryDal : NeoGenericRepositoryBase<Category>, ICategoryDal
    {
        private readonly IGraphClient _graphClient;

        public NeoCategoryDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public List<Category> GetCategoryTreeById(int id)
        {
            var category = _graphClient.Cypher
                .Match("(c:Category)<-[:INNER]-(s:SubCategory)")
                .Where((Category c) => c.Id == id)
                .Return((c, s, p, a) => new Category
                {
                    Id = c.As<Category>().Id,
                    CategoryName = c.As<Category>().CategoryName,
                    SubCategories = s.CollectAs<SubCategory>()
                })
                .ResultsAsync.Result.ToList();

            return category;
        }
    }
}
