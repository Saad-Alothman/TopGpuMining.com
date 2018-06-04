//using System.Data.Entity;
//using Microsoft.AspNet.Identity.EntityFramework;

//namespace CreaDev.Framework.Core.Models
//{
//    public class GenericSoftDeleteRepository<TDbContext, TUser> : GenericRepository<TDbContext, TUser>
//        where TDbContext : DbContext, new()
//        where TUser : IdentityUser
//    {
//        public override TEntity Update<TEntity>(TEntity originalEntity, TEntity entityToUpdate)
//        {
//            TDbContext context = _context ?? new TDbContext();

//            context.Entry(originalEntity).CurrentValues.SetValues(entityToUpdate);
            
//            (originalEntity as IAuditableCommon<TEntity>)?.UpdateAudit();

//            if (_context == null)
//            {
//                context.SaveChanges();
//                context.Dispose();
//            }

//            return originalEntity;
//        }

//        public override TEntity Update<TEntity>(TEntity entityToUpdate)
//        {
//            TDbContext context = _context ?? new TDbContext();

//            var dbSet = context.Set<TEntity>();

//            if (context.Entry(entityToUpdate).State == EntityState.Detached)
//                dbSet.Attach(entityToUpdate);

//            context.Entry(entityToUpdate).State = EntityState.Modified;

//            (entityToUpdate as IAuditableCommon<TEntity>)?.UpdateAudit();

//            if (_context == null)
//            {
//                context.SaveChanges();
//                context.Dispose();
//            }

//            return entityToUpdate;
//        }

//        public override void Delete<TEntity>(TEntity entityToDelete) where TEntity : EntityBase
//        {
//            TDbContext context = _context ?? new TDbContext();


//            var dbSet = context.Set<TEntity>();

//            if (context.Entry(entityToDelete).State == EntityState.Detached)
//            {
//                dbSet.Attach(entityToDelete);
//            }

//            dbSet.Remove(entityToDelete);

//            if (_context == null)
//            {
//                context.SaveChanges();
//                context.Dispose();
//            }
//        }

//        public override void Delete<TEntity>(object id) where TEntity : EntityBase
//        {
//            TDbContext context = _context ?? new TDbContext();

//            var dbSet = context.Set<TEntity>();

//            var found = dbSet.Find(id);

//            dbSet.Remove(found);

//            if (_context == null)
//            {
//                context.SaveChanges();
//                context.Dispose();
//            }
//        }
//    }
//}