using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Models;

namespace GpuMiningInsights.Domain.Models
{
    public class Brand : GmiEntityBase
    {

        //[Display(Name = nameof(Core.Resources.Domain.CostCenter_Name), ResourceType = typeof(Core.Resources.Domain))]
        public LocalizableTextRequired Name { get; set; }

        //[Display(Name = nameof(Core.Resources.Domain.Description), ResourceType = typeof(Core.Resources.Domain))]
        public LocalizableText Description { get; set; }

        //private List<GPU> Gpus { get; set; }
        public override void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Brand;
            this.Name = updateData.Name;
            this.Description = updateData.Description;

        }
    }
}
