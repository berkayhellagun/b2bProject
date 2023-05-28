using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Relationships
{
    public class INNER: BaseEntity
    {
        public int CategoryId { get; set; }
        public int SubCatId { get; set; }
        public int ProductId { get; set; }
    }
}
