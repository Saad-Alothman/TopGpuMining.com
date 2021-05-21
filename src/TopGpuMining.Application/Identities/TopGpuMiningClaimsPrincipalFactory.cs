using TopGpuMining.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TopGpuMining.Application.Identities
{
    public class TopGpuMiningClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public TopGpuMiningClaimsPrincipalFactory(TopGpuMiningUserManager userManager,
           RoleManager<Role> roleManager,
           IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var emailClaim = new Claim(ClaimTypes.Email, user.Email);

            identity.AddClaim(emailClaim);

            return identity;
        }
    }
}
