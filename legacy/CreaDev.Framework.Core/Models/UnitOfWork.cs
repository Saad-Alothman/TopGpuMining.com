using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CreaDev.Framework.Core.Models
{
    public class UnitOfWork<TDbContext, TUser> : IDisposable 
        where TDbContext:DbContext, new()
        where TUser : IdentityUser, new()
    {
        public readonly TDbContext Context = new TDbContext();


        private GenericRepository<TDbContext, TUser> _genericRepository;

        public GenericRepository<TDbContext, TUser> GenericRepository
        {
            get
            {
                if (_genericRepository == null)
                    _genericRepository = new GenericRepository<TDbContext, TUser>(Context);

                return _genericRepository;
            }

        }


        //private AccountRepository _accountRepositry;

        //public AccountRepository AccountRepository
        //{
        //    get
        //    {
        //        if (_accountRepositry == null)
        //            _accountRepositry = new AccountRepository(Context);

        //        return _accountRepositry;
        //    }
        //}


        public int Commit()
        {
            return Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}