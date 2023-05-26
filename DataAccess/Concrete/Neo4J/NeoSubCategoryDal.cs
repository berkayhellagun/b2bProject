using Core.DataAccess.Concrete.EntityFramework;
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
    public class NeoSubCategoryDal : NeoGenericRepositoryBase<SubCategory>, ISubCategoryDal
    {
        public NeoSubCategoryDal(IGraphClient graphClient) : base(graphClient)
        {
        }
    }
}
