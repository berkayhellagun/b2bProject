using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductSupplierId { get; set; }
        public int ProductCategoryId { get; set; }
        public string? ProductDescription { get; set; }
        public string ProductCountry { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public int ProductInStock { get; set; }
        public string? ProductImage { get; set; }
        public int? ProductSalesAmount { get; set; }
    }
}
