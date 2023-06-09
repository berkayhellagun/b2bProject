using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Relationships
{
    public class USER_OPERATION_CLAIM : BaseEntity
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
