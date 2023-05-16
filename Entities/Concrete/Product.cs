using Core.Entities;
using Core.Entities.Concrete;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Product : BaseEntity
    {
        //[Key]
        //public int ProductId { get; set; }
        [JsonProperty("pName")]
        public string ProductName { get; set; }
        public int ProductSupplierId { get; set; }
        public int ProductSubCategoryId { get; set; }
        public string? ProductDescription { get; set; }
        public string ProductCountry { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductInStock { get; set; }
        public int? ProductSalesAmount { get; set; }
    }
}
