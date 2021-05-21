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
    public  class PriceSource : GmiEntityBase
    {
        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public string Name { get; set; }

        [Display(Name = nameof(Common.Url), ResourceType = typeof(Common))]
        public string Url { get; set; }
        [Display(Name = nameof(Common.PriceSourceType), ResourceType = typeof(Common))]
        public PriceSourceType PriceSourceType { get; set; }

        public bool IsScrape = false;
        public bool RequiresJavascript = false;


        public override void Update(object objectWithNewData)
        {
            if (!(objectWithNewData is PriceSource updateData)) return;
            this.Name = updateData.Name;
            this.Url = updateData.Url;
            this.IsScrape = updateData.IsScrape;
            this.RequiresJavascript = updateData.RequiresJavascript;
            this.PriceSourceType = updateData.PriceSourceType;
        }
    }
}