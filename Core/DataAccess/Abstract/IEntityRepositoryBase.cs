using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepositoryBase<T> where T : class, IEntity, new()
    {
        List<T> GetAllDB(Expression<Func<T, bool>> filter = null);
        T GetDB(Expression<Func<T, bool>> filter);
        bool AddDB(T entity);
        bool UpdateDB(T entity);
        bool DeleteDB(T entity);

        Task<bool> AsyncAddDB(T entity);
        Task<bool> AsyncUpdateDB(T entity);
        Task<bool> AsyncDeleteDB(T entity);
        Task<List<T>> AsyncGetAllDB(Expression<Func<T, bool>> filter = null);
        Task<T> AsyncGetDB(Expression<Func<T, bool>> filter);
    }
}
