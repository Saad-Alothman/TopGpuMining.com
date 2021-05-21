using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable, IAsyncDisposable
    {
        public IGenericRepository GenericRepository { get; }

        int Commit();

        Task<int> CommitAsync();
    }
}
