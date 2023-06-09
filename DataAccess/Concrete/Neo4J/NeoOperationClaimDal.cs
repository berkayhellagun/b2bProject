using Core.DataAccess.Concrete.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoOperationClaimDal : NeoGenericRepositoryBase<OperationClaim>, IOperationClaimDal
    {
        private readonly IGraphClient _graphClient;
        public NeoOperationClaimDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }
    }
}
