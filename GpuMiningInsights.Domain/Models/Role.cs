using System;
using CreaDev.Framework.Core.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GpuMiningInsights.Domain.Models
{
    [Serializable]
    public class Role : IdentityRole
    {
        public LocalizableText Description { get; set; }


        public Role()
        {
            this.Description = new LocalizableText();
        }



    }
}