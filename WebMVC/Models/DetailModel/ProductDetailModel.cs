namespace WebMVC.Models.DetailModel
{
    public class ProductDetailModel
    {
        public string ProductName { get; set; }
        public int SellerId { get; set; }
        public string SellerNickName { get; set; }
        public int ProductCategoryId { get; set; }
        public string? ProductDescription { get; set; }
        public string ProductCountry { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductInStock { get; set; }
        public int? ProductSalesAmount { get; set; }
        public string? ProductImage { get; set; }
        public string CategoryName { get; set; }
    }
}
