using System.Data;

namespace CoinsApplication.DAL.Infrastructure
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork Create(IsolationLevel isolationLevel);
        
        IUnitOfWork Create();
    }
}