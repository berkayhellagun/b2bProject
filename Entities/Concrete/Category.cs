using Core.Entities;
using Core.Entities.Concrete;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Entities.Concrete
{
    public class Category : BaseEntity
    {
        //[Key]
        //public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<SubCategory>? SubCategories { get; set; }// = new List<SubCategory>();
    }
}
