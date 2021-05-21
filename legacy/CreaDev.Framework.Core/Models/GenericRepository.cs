using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CreaDev.Framework.Core.Models
{
    public class GenericRepository<TDbContext,TUser>  
        where TDbContext : DbContext, new()
        where TUser : IdentityUser
    {
        internal TDbContext _context;

        public GenericRepository() { }

        public GenericRepository(TDbContext context)
        {
            this._context = context;

        }

        public virtual List<TEntity> Create<TEntity>(List<TEntity> entites) where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();

            var dbSet = context.Set<TEntity>();

            foreach (var entity in entites)
            {
                if (entity is IAuditable<TUser>)
                    (entity as IAuditableCommon<TEntity>)?.InsertAudit();
                dbSet.Add(entity);
            }

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entites;
        }

        public virtual TEntity Create<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();

            var dbSet = context.Set<TEntity>();

            (entity as IAuditableCommon<TEntity>)?.InsertAudit();

            

            dbSet.Add(entity);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entity;
        }

        public virtual TEntity Update<TEntity>(TEntity originalEntity, TEntity entityToUpdate) where TEntity : class
        {
            TDbContext context = _context ?? new TDbContext();

            context.Entry(originalEntity).CurrentValues.SetValues(entityToUpdate);

            (originalEntity as IAuditableCommon<TEntity>)?.UpdateAudit();

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return originalEntity;
        }

        public virtual TEntity Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            TDbContext context = _context ?? new TDbContext();

            var dbSet = context.Set<TEntity>();

            if (context.Entry(entityToUpdate).State == EntityState.Detached)
                dbSet.Attach(entityToUpdate);

            context.Entry(entityToUpdate).State = EntityState.Modified;

            (entityToUpdate as IAuditableCommon<TEntity>)?.UpdateAudit();

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entityToUpdate;
        }

        public virtual void Delete<TEntity>(TEntity entityToDelete) where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();


            var dbSet = context.Set<TEntity>();

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

        public virtual void Delete<TEntity>(object id) where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();

            var dbSet = context.Set<TEntity>();

            var found = dbSet.Find(id);

            dbSet.Remove(found);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }

        public virtual int Count<TEntity>() where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();

            var dbSet = context.Set<TEntity>();

            int count;

            count = dbSet.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual int Count<TEntity>(SearchCriteria<TEntity> search) where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            int count = 0;

            if (search.FilterExpression != null)
            {
                query = query.Where(search.FilterExpression);
            }

            count = query.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual SearchResult<TEntity> Search<TEntity>(SearchCriteria<TEntity> searchCriteria,
            List<string> includes) where TEntity : EntityBase
        {
            return Search(searchCriteria, includes.ToArray());
        }
        public virtual SearchResult<TEntity> Search<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : EntityBase
        {


            TDbContext context = _context ?? new TDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;


            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }
            else
            {
                query = query.OrderByDescending(entity => entity.Id);
            }

            SearchResult<TEntity> result = new SearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = query.Count(),
            };

            query = Queryable.Take<TEntity>(query.Skip(searchCriteria.StartIndex), searchCriteria.PageSize);

            result.Result = query.ToList();


            if (_context == null)
                context.Dispose();


            return result;

        }

        public virtual TEntity GetByID<TEntity>(params object[] keys) where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();

            TEntity entity = context.Set<TEntity>().Find(keys);

            return entity;

        }
        public virtual TEntity GetById<TEntity>(int id, List<string> includes) where TEntity : EntityBase
        {
            return Search(new SearchCriteria<TEntity>(entity => entity.Id == id), includes).Result.FirstOrDefault();

        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            List<string> includeProperties = null, int? maxSize = null) where TEntity : EntityBase
        {
            string[] includes = includeProperties != null ? includeProperties.ToArray(): null;
            return Get(filter, orderBy, includes, maxSize);
        }


        private IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string[] includeProperties = null, int? maxSize = null) where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();

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

        public List<TEntity> GetAll<TEntity>() where TEntity : EntityBase
        {
            TDbContext context = _context ?? new TDbContext();
            return context.Set<TEntity>().ToList();
        }
    }
}
