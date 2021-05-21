using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TopGpuMining.Domain.Models
{
    public class Role : IdentityRole , IBaseEntity , ISeedableEntity<Role>
    {
        public LocaleString Description { get; private set; } = new LocaleString();

        public Role Update(Role entity)
        {
            Description.Arabic = entity.Description.Arabic;
            Description.English = entity.Description.English;
            
            return this;
        }
    }
}
