using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Relationships
{
    public class CONCERNED : BaseEntity
    {
        public int PersonId { get; set; }
        public int SectorId { get; set; }
    }
}
