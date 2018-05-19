using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Helpers;
using CreaDev.Framework.Core.Resources;
using CreaDev.Framework.Core.Utils;
using GpuMiningInsights.Domain.Models;
using Microsoft.AspNet.Identity;

namespace GpuMiningInsights.Application.Identity
{
    public static class Extensions
    {
        public static EmployeeInfo GetEmployeeInfo(this IIdentity identity)
        {
            EmployeeInfo result = null;

            var claimsIdentity = (ClaimsIdentity) identity;

            var employeeInfoJson = claimsIdentity.FindFirstValue(EmployeeInfo.CalimName);

            if (!String.IsNullOrWhiteSpace(employeeInfoJson))
                result = TryHelper.Try(() => Serialization.DeSerialize<EmployeeInfo>(employeeInfoJson));

            return result;
        }

        public static async Task<ClaimsIdentity> GenerateUserIdentityAsync(this User user, UserManager<User> manager)
        {

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            userIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));

            return userIdentity;
        }



    }
}
