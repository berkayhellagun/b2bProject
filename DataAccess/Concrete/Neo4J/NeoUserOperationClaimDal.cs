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
    public class NeoUserOperationClaimDal : NeoGenericRepositoryBase<UserOperationClaim>, IUserOperationClaimDal
    {
        private IGraphClient _graphClient;
        public NeoUserOperationClaimDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public List<UserOperationClaimDetail> GetDetailByIdDB(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserOperationClaimDetail> GetDetailDB()
        {
            throw new NotImplementedException();
        }
    }
}
