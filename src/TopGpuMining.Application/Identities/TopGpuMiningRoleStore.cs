using TopGpuMining.Domain.Models;
using TopGpuMining.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Application.Identities
{
    public class TopGpuMiningRoleStore : RoleStore<Role>
    {
        public TopGpuMiningRoleStore(TopGpuMiningDbContext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
        {

        }
    }
}
