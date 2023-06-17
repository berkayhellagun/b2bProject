using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class OrderDetail : BaseEntity
    {
        public Order Order { get; set; }
        public Person Buyer { get; set; }
        public Person Seller { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Property>? Properties { get; set; }
    }
}
