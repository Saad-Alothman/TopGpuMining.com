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
    public class Algorithm : GmiEntityBase
    {

        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public LocalizableTextRequired Name { get; set; }

        [Display(Name = nameof(Common.Description), ResourceType = typeof(Common))]

        public LocalizableText Description { get; set; }

        //private List<GPU> Gpus { get; set; }
        public override void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Algorithm;
            this.Name = updateData.Name;
            this.Description = updateData.Description;

        }
    }
}
