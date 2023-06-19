namespace WebMVC.Models
{
    public class MostOrderedProductModel
    {
        public ProductDetail Item1 { get; set; }
        public int Item2 { get; set; }
    }

    public class ProductDetail
    {
        public string ProductName { get; set; }
        public object ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public int ProductInStock { get; set; }
        public List<Property> Properties { get; set; }
        public int Id { get; set; }
    }

    public class Property
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
    }
}
