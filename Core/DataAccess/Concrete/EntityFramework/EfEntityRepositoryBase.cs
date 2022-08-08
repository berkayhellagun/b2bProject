using Core.DataAccess.Abstract;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Transactions;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepositoryBase<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private bool AddDB(TEntity entity)
        {
            bool Status = false;
            using (var db = new TContext())
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    try
                    {
                        var entityEntry = db.Entry(entity);
                        entityEntry.State = EntityState.Added;
                        Status = db.SaveChanges() > 0; //save changes return integer value
                        transaction.Complete();
                        return Status;
                    }
                    catch (Exception)
                    {
                        transaction.Dispose();
                        return Status;
                    }
                }
            }
        }
        public Task<bool> AsyncAddDB(TEntity entity)
        {
            return Task.Run(() => { return AddDB(entity); });
        }

        private bool DeleteDB(TEntity entity)
        {
            using (var db = new TContext())
            {
                var entityEntry = db.Entry(entity);
                entityEntry.State = EntityState.Deleted;
                return db.SaveChanges() > 0;
            }
        }
        public Task<bool> AsyncDeleteDB(TEntity entity)
        {
            return Task.Run(() => { return DeleteDB(entity); });
        }

        private TEntity GetDB(Expression<Func<TEntity, bool>> filter)
        {
            using (var db = new TContext())
            {
#pragma warning disable // Possible null reference return.
                return db.Set<TEntity>().FirstOrDefault(filter);
#pragma warning restore // Possible null reference return.
            }
        }
        public Task<TEntity> AsyncGetDB(Expression<Func<TEntity, bool>> filter)
        {
            return Task.Run(() => { return GetDB(filter); });
        }

        private List<TEntity> GetAllDB(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var db = new TContext())
            {
                return filter == null
                    ? db.Set<TEntity>().ToList()
                    : db.Set<TEntity>().Where(filter).ToList();
            }
        }
        public Task<List<TEntity>> AsyncGetAllDB(Expression<Func<TEntity, bool>> filter = null)
        {
            return Task.Run(() => { return GetAllDB(filter); });
        }

        private bool UpdateDB(TEntity entity)
        {
            using (var db = new TContext())
            {
                var entityUpdated = db.Entry(entity);
                entityUpdated.State = EntityState.Modified;
                return db.SaveChanges() > 0;
            }
        }
        public Task<bool> AsyncUpdateDB(TEntity entity)
        {
            return Task.Run(() => { return UpdateDB(entity); });
        }

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var db = new TContext())
            {
                return filter == null
                   ? db.Set<TEntity>().ToList()
                   : db.Set<TEntity>().Where(filter).ToList();
            }
        }
    }
}
