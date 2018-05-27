using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Core.Resources;

namespace GpuMiningInsights.Domain.Models
{
    public class GPUPriceSource : GmiEntityBase
    {
        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public string Name { get; set; }

        [Display(Name = nameof(Common.Description), ResourceType = typeof(Common))]
        public string Desc { get; set; }
        
        public PriceSource PriceSource { get; set; }
        public int? PriceSourceId { get; set; }

        public Gpu Gpu { get; set; }
        public int? GpuId { get; set; }

        public override void Update(object objectWithNewData)
        {
            if (!(objectWithNewData is GPUPriceSource updateData)) return;
            this.Name = updateData.Name;
            this.Desc = updateData.Desc;
            this.PriceSourceId = updateData.PriceSourceId;
            this.GpuId = updateData.GpuId;
        }
    }
}