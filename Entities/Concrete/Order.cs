using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Order : BaseEntity
    {
        public int ProductQuantity { get; set; }
        public int ProductId { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
    }
}
