namespace WebMVC.Models.AddModel
{
    public class OrderAddModel
    {
        public int ProductQuantity { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
