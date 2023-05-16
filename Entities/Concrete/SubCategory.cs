using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class SubCategory: BaseEntity
    {
        //[Key]
        //public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public List<Product>? Products { get; set; }// = new List<Product>();
    }
}
