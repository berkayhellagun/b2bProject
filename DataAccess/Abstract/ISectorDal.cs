using Core.DataAccess.Abstract;
using Core.Entities.Concrete;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ISectorDal : IEntityRepositoryBase<Sector>
    {
        List<Person> GetFirmGroupBySector();

        List<Person> GetFirmGroupBySectorBySectorId(int sectorId);
    }
}
