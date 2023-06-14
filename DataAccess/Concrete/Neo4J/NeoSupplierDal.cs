using Core.DataAccess.Concrete.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Neo4jClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Neo4J
{
    public class NeoSupplierDal : NeoGenericRepositoryBase<Supplier>, ISupplierDal
    {
        private readonly IGraphClient _graphClient;
        public NeoSupplierDal(IGraphClient graphClient) : base(graphClient)
        {
            _graphClient = graphClient;
        }

        public SupplierDetail GetSupplierDetail(int id)
        {
            throw new NotImplementedException();
        }
    }
}
