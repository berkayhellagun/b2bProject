namespace WebMVC.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int ProductSupplierId { get; set; }
        public int ProductCategoryId { get; set; }
        public string? ProductDescription { get; set; }
        public string ProductCountry { get; set; }
        public double ProductPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductInStock { get; set; }
        public string? ProductImage { get; set; }
        public int? ProductSalesAmount { get; set; } = 0;
        public List<PropertyModel>? Properties { get; set; }
    }

    public class PropertyModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string? Value { get; set; }
    }
}
