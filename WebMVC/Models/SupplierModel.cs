
namespace WebMVC.Models
{
    public class SupplierModel
    {
        public int Id { get; set; }
        public string SupplierName { get; set; }
        public string SupplierMail { get; set; }
        public string SupplierTelephoneNumber { get; set; }
        public int SupplierContactPersonId { get; set; }
        public double? SupplierGiro { get; set; }
        public string SupplierCountry { get; set; }
        public string SupplierFoundYear { get; set; }
        public string EmployeeCount { get; set; }
        public string SupplierTaxNo { get; set; }
        public string SupplierDescription { get; set; }
        public int CategoryId { get; set; }
        public string? SupplierImage { get; set; }

    }
}
