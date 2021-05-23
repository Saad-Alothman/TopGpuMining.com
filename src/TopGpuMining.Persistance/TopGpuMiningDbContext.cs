using TopGpuMining.Core;
using TopGpuMining.Core.Exceptions;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TopGpuMining.Persistance
{
    public class TopGpuMiningDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Country> Countries { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Algorithm> Algorithms { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Gpu> Gpus { get; set; }
        public virtual DbSet<Hashrate> Hashrates { get; set; }
        public virtual DbSet<PriceSource> PriceSources { get; set; }
        public virtual DbSet<GpuPriceSource> GpuPriceSources { get; set; }
        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<FiatCurrency> FiatCurrencies { get; set; }
        public virtual DbSet<GpusInsightsReport> GpusInsightsReports { get; set; }

        public TopGpuMiningDbContext()
        {

        }

        public TopGpuMiningDbContext(DbContextOptions<TopGpuMiningDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;
            
            var connectionString = AppSettings.Configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            builder.Entity<User>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            
        }


        public void EnsureSeeding()
        {
            Seed(Countries, a => a.Id);

            Seed(Roles, a => a.Id);

            Seed(Addresses, a => a.Id);

            SeedDefaultAdminUser();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex) when (ex.InnerException is SqlException)
            {
                var sqlException = ex.InnerException as SqlException;

                if (sqlException.Number == 547)
                    throw new BusinessException(MessageText.DatabaseRelatedItemOnDelete);
                else
                    throw ex;
            }
        }

        public void SeedDefaultAdminUser()
        {
            var user = GetDefaultUser();

            var userManager = ServiceLocator.Current.GetService<ITopGpuMiningUserManager<User>>();

            var found = userManager.FindByIdAsync(user.Id).GetAwaiter().GetResult();

            if (found == null)
            {
                userManager.CreateAsync(user, "1122").GetAwaiter().GetResult();
                userManager.AddToRoleAsync(user, AppRoles.ADMIN_ROLE).GetAwaiter().GetResult();
            }
            else
            {
                found.Update(user);

                userManager.UpdateAsync(found).GetAwaiter().GetResult();

                var token = userManager.GeneratePasswordResetTokenAsync(found).GetAwaiter().GetResult();

                userManager.ResetPasswordAsync(found, token, "1122").GetAwaiter().GetResult();

                userManager.AddToRoleAsync(found, AppRoles.ADMIN_ROLE).GetAwaiter().GetResult();

            }

        }

        private void Seed<T, T2>(DbSet<T> dataSet, Func<T, T2> uniquProperty) where T : class, ISeedableEntity<T>
        {
            var separtor = Path.DirectorySeparatorChar;

            var tableName = Model.FindEntityType(typeof(T)).GetTableName();

            var baseDir = Assembly.GetExecutingAssembly().Location;
            baseDir = Path.GetDirectoryName(baseDir);

            var filePath = $"{baseDir}{separtor}Data{separtor}{tableName}.json";

            if (File.Exists(filePath))
            {
                var data = File.ReadAllText(filePath);

                var items = JsonConvert.DeserializeObject<List<T>>(data);

                foreach (var item in items)
                {
                    var key = uniquProperty(item);

                    var found = dataSet.Find(key);

                    if (found != null)
                    {
                        found = found.Update(item);
                        dataSet.Update(found);
                    }
                    else
                        dataSet.Add(item);
                }


                SaveChanges();
            }
        }

        private User GetDefaultUser()
        {
            var id = "98c47e9a-dc55-4030-88d9-e21f81743ce9";
            var email = "admin@admin.com";

            var user = new User()
            {
                Id = id,
                Email = email,
                UserName = email,
            };

            return user;
        }

    }
}
