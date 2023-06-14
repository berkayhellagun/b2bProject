using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductWithProperties
    {
        public IEnumerable<Property>? Properties { get; set; }
    }
}
