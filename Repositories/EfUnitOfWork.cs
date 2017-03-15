using System;
using System.Data.Entity;

namespace Repositories
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext dbContext;

        public EfUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
