using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core.Resources;

namespace GpuMiningInsights.Domain.Models
{
    //Todo: Rename To ChipsetAlgorithmHashrate
    public class Hashrate : GmiEntityBase
    {
        [ForeignKey("AlogrthimId")]
        public Algorithm Algorithm { get; set; }
        public int? AlogrthimId { get; set; }
      
        public Model Model { get; set; }
        public int? ModelId { get; set; }

    //Todo: Rename To HashrateValueMhz
        [Display(Name = nameof(Common.HashrateValue), ResourceType = typeof(Common))]
        public string HashrateNumber { get; set; }

        [Display(Name = nameof(Common.Description), ResourceType = typeof(Common))]

        public string Description { get; set; }

        


        public override void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Hashrate;
            this.HashrateNumber = updateData.HashrateNumber;
            this.Description = updateData.Description;
            this.AlogrthimId = updateData.AlogrthimId;
            this.ModelId = updateData.ModelId;

        }


    }
}
