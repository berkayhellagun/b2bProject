using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class ProductDetails : BaseEntity
    {
        
        public string CategoryName{ get; set; }
        public string SubCategoryName { get; set; }
        public string ProductName { get; set; }
        public DateTime ProductionDate { get; set; }
        public decimal ProductPrice { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductInStock { get; set; }
        public string ProductCountry { get; set; }
        public int SellerId { get; set; }
        public string SellerNickName { get; set; }
        public IEnumerable<Property>? Properties { get; set; }
        
    }
}
