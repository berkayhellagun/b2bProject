namespace WebMVC.Models.DetailModel
{
    public class SupplierDetailModel
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierMail { get; set; }
        public string SupplierTelephoneNumber { get; set; }
        public int SupplierContactPersonId { get; set; }
        public string PersonName { get; set; }
        public string PersonEmail { get; set; }
        public decimal SupplierGiro { get; set; }
        public string SupplierCountry { get; set; }
        public string SupplierFoundYear { get; set; }
        public string EmployeeCount { get; set; }
        public string SupplierTaxNo { get; set; }
        public string SupplierDescription { get; set; }
        public string? SupplierImage { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
