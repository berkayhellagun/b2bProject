using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class SupplierDetail : Supplier
    {
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }
        public string CategoryName { get; set; }
    }
}
