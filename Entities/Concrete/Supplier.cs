using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Supplier : IEntity
    {
        [Key]
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierMail { get; set; }
        public string SupplierTelephoneNumber { get; set; }
        public int SupplierContactPersonId { get; set; }
        public decimal SupplierGiro { get; set; }
        public string SupplierCountry { get; set; }
        public string SupplierFoundYear { get; set; }
        public string EmployeeCount { get; set; }
        public string SupplierTaxNo { get; set; }
        public string SupplierDescription { get; set; }
    }
}
