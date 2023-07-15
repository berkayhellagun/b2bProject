using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepositoryBase<T> where T : BaseEntity, new()
    {
        Task<bool> AsyncAddDB(T entity);
        Task<bool> AsyncUpdateDB(T entity);
        Task<bool> AsyncDeleteDB(T entity);
        Task<List<T>> AsyncGetAllDB(Expression<Func<T, bool>> filter = null);
        Task<T> AsyncGetDB(Dictionary<string, object> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
    }
}
