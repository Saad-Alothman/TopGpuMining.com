using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Persistance;
using User = GpuMiningInsights.Domain.Models.User;

namespace REME.Persistance
{
    public class AccountRepository
    {
        internal GmiContext _context;

        public AccountRepository() { }

        public AccountRepository(GmiContext context)
        {
            this._context = context;

        }

        
        

        

        

        public User GetByID(string userId)
        {
            GmiContext context = _context ?? new GmiContext();

            var result = context.Users
                .Include(a => a.Roles)
                .Where(a => a.Id == userId).FirstOrDefault();

            if (_context == null)
                context.Dispose();

            return result;

        }

        public List<Role> GetRoles()
        {
            GmiContext context = _context ?? new GmiContext();

            var result = context.Roles.ToList();

            if (_context == null)
                context.Dispose();

            return result;

        }


        public virtual TEntity Create<TEntity>(TEntity entity) where TEntity : class, new()
        {
            GmiContext context = _context ?? new GmiContext();

            var dbSet = context.Set<TEntity>();

            //if (entity is AuditableEntity<User>)
            //    (entity as AuditableEntity).InsertAudit();


            dbSet.Add(entity);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entity;
        }

        public virtual void Delete<TEntity>(object id) where TEntity : class, new()
        {
            GmiContext context = _context ?? new GmiContext();

            var dbSet = context.Set<TEntity>();

            var found = dbSet.Find(id);

            dbSet.Remove(found);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }
        public virtual void Delete(User entityToDelete) 
        {
            GmiContext context = _context ?? new GmiContext();
            


            var dbSet = context.Set<User>();

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }
        public virtual int Count<TEntity>() where TEntity : class, new()
        {
            GmiContext context = _context ?? new GmiContext();

            var dbSet = context.Set<TEntity>();

            int count;

            count = dbSet.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

  

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includeProperties = null, int? maxSize = null) where TEntity : class, new()
        {
            GmiContext context = _context ?? new GmiContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                if (maxSize.HasValue)
                    query = orderBy(query);
                else
                    query = orderBy(query);
            }
            else
            {
                if (maxSize.HasValue)
                    query = query.Take(maxSize.Value);
            }

            var result = query.ToList();

            if (_context == null)
                context.Dispose();

            return result;
        }

        public virtual AccountSearchResult<TEntity> SearchUser<TEntity>(AccountSearchCriteria<TEntity> searchCriteria) where TEntity : User, new()
        {
            GmiContext context = _context ?? new GmiContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;


            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }
            else
            {
                query = query.OrderByDescending(entity => entity.Id);
            }

            AccountSearchResult<TEntity> result = new AccountSearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = query.Count(),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = query.ToList();


            if (_context == null)
                context.Dispose();


            return result;
        }
        public virtual SearchResult<User> SearchUser(SearchCriteria<User> searchCriteria,params string[] includes) 
        {
            GmiContext context = _context ?? new GmiContext();

            var dbSet = context.Set<User>();
 


            IQueryable<User> query = dbSet;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }
            else
            {
                query = query.OrderByDescending(entity => entity.Id);
            }
            
            SearchResult<User> result = new SearchResult<User>(searchCriteria)
            {
                TotalResultsCount = query.Count(),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = query.ToList();


            if (_context == null)
                context.Dispose();


            return result;
        }


        public virtual AccountSearchResult<TEntity> Search<TEntity>(AccountSearchCriteria<TEntity> searchCriteria, bool all = false) where TEntity : class, new()
        {
            GmiContext context = _context ?? new GmiContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;


            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }


            AccountSearchResult<TEntity> result = new AccountSearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = query.Count(),
            };

            if (!all)
                query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);


            result.Result = query.ToList();


            if (_context == null)
                context.Dispose();


            return result;
        }
    }
}