using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class UserOperationClaim
    {
        [Key]
        public int UserOperationClaimId { get; set; }
        public int UserId { get; set; }
        public int OperationId { get; set; }
    }
}
