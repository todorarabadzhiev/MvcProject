using System;
using System.Data.Entity;

namespace EFositories
{
    public class EfUnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext dbContext;

        public EfUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit()
        {
            int result = this.dbContext.SaveChanges();

            return result;
        }

        public void Dispose()
        {
        }
    }
}
