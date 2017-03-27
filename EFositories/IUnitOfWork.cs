using System;

namespace EFositories
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();
    }
}
