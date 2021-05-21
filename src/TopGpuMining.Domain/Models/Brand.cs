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
    public class Brand : BaseEntity
    {

        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        public LocaleString Name { get; set; }

        [Display(Name = nameof(CommonText.Description), ResourceType = typeof(CommonText))]

        public LocaleString Description { get; set; }

        //private List<GPU> Gpus { get; set; }
        public void Update(Brand updateData)
        {
            this.Name = updateData.Name;
            this.Description = updateData.Description;

        }
    }
}
