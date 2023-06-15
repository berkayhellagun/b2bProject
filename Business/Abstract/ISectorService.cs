using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISectorService : IGenericService<Sector>
    {
        IDataResult<List<Person>> GetFirmGroupBySector();
        IDataResult<List<Person>> GetFirmGroupBySectorBySectorId(int sectorId);
    }
}
