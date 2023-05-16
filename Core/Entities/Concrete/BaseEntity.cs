using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class BaseEntity:IEntity
    {
        public int Id { get; set; }
    }
}
