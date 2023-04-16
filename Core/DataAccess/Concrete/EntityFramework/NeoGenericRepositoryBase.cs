using Core.DataAccess.Abstract;
using Core.Entities;
using System.Linq.Expressions;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class NeoGenericRepositoryBase<TEntity> : IEntityRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
    {
        public Task<bool> AsyncAddDB(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AsyncDeleteDB(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> AsyncGetAllDB(Expression<Func<TEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> AsyncGetDB(Expression<Func<TEntity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AsyncUpdateDB(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
