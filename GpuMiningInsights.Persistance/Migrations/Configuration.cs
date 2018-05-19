using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GpuMiningInsights.Persistance.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GpuMiningInsights.Persistance.GmiContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(GpuMiningInsights.Persistance.GmiContext context)
        {
            SeedUsers(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private void SeedUsers(GmiContext context)
        {
            context.Roles.AddOrUpdate(a=>a.Id, new Role()
            {
                Name = "Admin",
                Id = "Admin",
                
            });
            

            string password = new PasswordHasher().HashPassword("1122");
            string userId = "E428D8D6-4C5E-4D33-9437-569195B3B80A".ToLower();
            context.Users.AddOrUpdate(a => a.Id,
                new User()
                {
                    Id = userId.ToLower(),
                    UserName = "user@user.com",
                    Email = "user@user.com",
                    FirstName = new LocalizableTextRequired("User", "User"),
                    LastName = new LocalizableTextRequired("User", "User"),
                    MiddleName = new LocalizableText("User", "User"),
                    PasswordHash = password,
                    PhoneNumber = "0533333271",
                    SecurityStamp = Guid.NewGuid().ToString("D")
                });

         
        }
    }
}
