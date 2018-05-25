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
    public class PriceSource : GmiEntityBase
    {

        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public LocalizableTextRequired Name { get; set; }

        [Display(Name = nameof(Common.Url), ResourceType = typeof(Common))]

        public LocalizableText Url { get; set; }

        public bool IsScrape = false;
        public bool RequiresJavascript = false;
        public  enum  PriceSourceType
        {
            AmazonUs=0, AmazonUk=1, AmazonCanada=2, AmazonIndia=3, NewEgg=4
        }
        public override void Update(object objectWithNewData)
        {
            if (!(objectWithNewData is PriceSource updateData)) return;
            this.Name = updateData.Name;
            this.Url = updateData.Url;
            this.IsScrape = updateData.IsScrape;
            this.RequiresJavascript = updateData.RequiresJavascript;
        }
    }
}
