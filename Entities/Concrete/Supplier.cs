using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Supplier : IEntity
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierMail { get; set; }
        public string SupplierTelephoneNumber { get; set; }
        public int SupplierContactPersonId { get; set; }
        //how can i implement work day and work hour
        public double? SupplierGiro { get; set; }
        public string SupplierCountry { get; set; }
        public string SupplierFoundYear { get; set; }
        public string EmployeeCount { get; set; }
        public string SupplierTaxNo { get; set; }
    }
}
