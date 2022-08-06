using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetail : Product
    {
        public string SupplierName { get; set; }
        public string CategoryName { get; set; }
    }
}
