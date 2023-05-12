using Core.Entities;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Entities.Concrete
{
    public class Category : IEntity
    {
        [Key]
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public List<SubCategory> SubCategories { get; set; }// = new List<SubCategory>();
    }
}
