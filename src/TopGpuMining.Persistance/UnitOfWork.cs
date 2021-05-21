using TopGpuMining.Core.Interfaces;
using TopGpuMining.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TopGpuMiningDbContext _context = new TopGpuMiningDbContext();
        private bool _disposed = false;
        private IGenericRepository _genericRepository;

        public IGenericRepository GenericRepository
        {
            get
            {
                if (_genericRepository == null)
                    _genericRepository = new GenericRepository(_context);

                return _genericRepository;
            }
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
            }

            this._disposed = true;
            GC.SuppressFinalize(this);
        }

        public ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                _disposed = true;
                return _context.DisposeAsync();
            }

            return new ValueTask(Task.CompletedTask);
        }
    }
}
