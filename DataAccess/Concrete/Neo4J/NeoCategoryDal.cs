using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Neo4jClient;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoCategoryDal : NeoGenericRepositoryBase<Category>, ICategoryDal
    {
        public NeoCategoryDal(IGraphClient graphClient) : base(graphClient)
        {
        }
    }
}
