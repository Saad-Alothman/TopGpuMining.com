using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Resources;

namespace TopGpuMining.Domain.Models
{
    public class Algorithm : BaseEntity
    {

        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        public LocaleString Name { get; set; }

        [Display(Name = nameof(CommonText.Description), ResourceType = typeof(CommonText))]

        public LocaleString Description { get; set; }

        public Algorithm()
        {
            this.Name = new LocaleString();
            this.Description = new LocaleString();
        }

        public void Update(Algorithm updateData)
        {
            this.Name = updateData.Name;
            this.Description = updateData.Description;

        }
    }
}
