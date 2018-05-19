using System;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Persistance;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GpuMiningInsights.Application.Identity
{
    public class CHRSUserStore<TUser> : UserStore<TUser, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>, IUserStore<TUser>, IUserStore<TUser, string>, IDisposable where TUser : IdentityUser
    {

        public CHRSUserStore(GmiContext context) : base(context)
        {
        }
    }

    public class CHRSRoleManager : RoleManager<Role, string>
    {
        public CHRSRoleManager(IRoleStore<Role, string> roleStore) : base(roleStore)
        {

        }
    }
}