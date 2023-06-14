using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Relationships
{
    public class PROPERTY : BaseEntity
    {
        public int ProductId { get; set; }
        public int PropertyId { get; set; }
    }
}
