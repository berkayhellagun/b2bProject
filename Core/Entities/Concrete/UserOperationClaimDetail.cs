using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class UserOperationClaimDetail
    {
        public int UserOperationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OperationId { get; set; }
        public string OperationName { get; set; }
    }
}
