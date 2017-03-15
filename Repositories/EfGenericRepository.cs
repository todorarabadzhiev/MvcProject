using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Repositories
{
    public class EFGenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        public EFGenericRepository(DbContext dbContext)
        {
            this.DbContext = dbContext;
            this.DbSet = dbContext.Set<T>();
        }

        public DbContext DbContext { get; set; }

        protected IDbSet<T> DbSet { get; set; }

        public void Add(T entity)
        {
            //var entry = AttachIfDetached(entity);
            //entry.State = EntityState.Added;
            this.DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public IEnumerable<T> GetAll()
        {
            return this.GetAll(null);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression)
        {
            return this.GetAll<object>(filterExpression, null);
        }

        public IEnumerable<T> GetAll<T1>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression)
        {
            return this.GetAll<T1, T>(filterExpression, sortExpression, null);
        }

        public IEnumerable<T2> GetAll<T1, T2>(Expression<Func<T, bool>> filterExpression, Expression<Func<T, T1>> sortExpression, Expression<Func<T, T2>> selectExpression)
        {
            IQueryable<T> result = this.DbSet;

            if (filterExpression != null)
            {
                result = result.Where(filterExpression);
            }

            if (sortExpression != null)
            {
                result = result.OrderBy(sortExpression);
            }

            if (selectExpression != null)
            {
                return result.Select(selectExpression).ToList();
            }
            else
            {
                return result.OfType<T2>().ToList();
            }
        }

        public T GetById(object id)
        {
            return this.DbSet.Find(id);
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            return entry;
        }
    }
}
