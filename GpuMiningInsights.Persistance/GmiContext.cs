using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using User = GpuMiningInsights.Domain.Models.User;

namespace GpuMiningInsights.Persistance
{
    public class GmiContext : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Algorithm> Algorithms { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Gpu> Gpus { get; set; }
        public virtual DbSet<Hashrate> Hashrates { get; set; }
        public virtual DbSet<PriceSource> PriceSources { get; set; }
        public virtual DbSet<GPUPriceSource> GpuPriceSources { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

        public GmiContext() : base("DefaultConnection")
        {
            this.Configuration.ProxyCreationEnabled = false;
            

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("Claims");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("Logins");

         

        }

        public override int SaveChanges()
        {

            //  WriteChangeLog();
            try
            {
                UpdateAuditableEntities();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {

                var exception = new Exception();
                string entitiesValidationErrors = "";
                DbEntityValidationException dbEntityValidationException = (DbEntityValidationException)ex;
                foreach (var entityValidationError in dbEntityValidationException.EntityValidationErrors)
                {

                    string entityValidationErrors = entityValidationError.Entry.Entity.GetType()+" :"+Environment.NewLine;
                    foreach (var dbValidationError in entityValidationError.ValidationErrors)
                    {
                        entityValidationErrors += dbValidationError.PropertyName + ":" + dbValidationError.ErrorMessage + " " + Environment.NewLine;
                    }

                    entitiesValidationErrors += entityValidationErrors + " " + Environment.NewLine;
                }

                exception = new Exception(entitiesValidationErrors);
                throw exception;

            }
   

            //try
            //{
            //    return base.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    throw new ValidationException(ex);
            //}

        }


        private void UpdateAuditableEntities()
        {
            var changedEntities = ChangeTracker.Entries();

            foreach (DbEntityEntry changedEntity in changedEntities)
            {
                if (changedEntity.Entity is IAuditableCommon<User>)
                    AuditEntityAcition((IAuditableCommon<User>)changedEntity.Entity, changedEntity.State);
            }
        }

        private static void AuditEntityAcition(IAuditableCommon<User> changedEntity, EntityState state)
        {
            IAuditableCommon<User> auditableEntity = (IAuditableCommon<User>)changedEntity;

            switch (state)
            {
                case EntityState.Added:
                    auditableEntity.InsertAudit();
                    break;

                case EntityState.Modified:
                    auditableEntity.UpdateAudit();
                    break;
            }
        }
    }
}
