using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Core.Resources;

namespace GpuMiningInsights.Domain.Models
{
    //TODO: Rename to Chipset
    public class Model : GmiEntityBase
    {

        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public LocalizableTextRequired Name { get; set; }

        [Display(Name = nameof(Common.Description), ResourceType = typeof(Common))]

        public LocalizableText Description { get; set; }

        public List<Hashrate> HashRates { get; set; }
        //private List<GPU> Gpus { get; set; }
        public override void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Model;
            this.Name = updateData.Name;
            this.Description = updateData.Description;

        }
    }
}