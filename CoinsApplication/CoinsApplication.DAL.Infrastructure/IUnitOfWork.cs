using System;

namespace CoinsApplication.DAL.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
