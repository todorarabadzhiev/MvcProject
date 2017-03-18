using System;

namespace EFositories
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
