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
    public class GpuPriceSource    : BaseEntity
    {
        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        public string Name { get; set; }

        [Display(Name = nameof(CommonText.Description), ResourceType = typeof(CommonText))]
        public string Desc { get; set; }
        
        public PriceSource PriceSource { get; set; }
        public string PriceSourceId { get; set; }

        public Gpu Gpu { get; set; }
        public string GpuId { get; set; }

        public void Update(GpuPriceSource updateData)
        {
            this.Name = updateData.Name;
            this.Desc = updateData.Desc;
            this.PriceSourceId = updateData.PriceSourceId;
            this.GpuId = updateData.GpuId;
        }
    }
}