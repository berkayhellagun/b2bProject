using Core.Utilities.Results.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISubCategoryService: IGenericService<SubCategory>
    {
        Task<IResult> connectCategory(int subId, int catId);
    }
}
