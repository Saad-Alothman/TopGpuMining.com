using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Resources;

namespace TopGpuMining.Domain.Models
{
    //TODO: Rename to Chipset
    public class Model : BaseEntity
    {

        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        public LocaleString Name { get; set; }

        [Display(Name = nameof(CommonText.Description), ResourceType = typeof(CommonText))]

        public LocaleString Description { get; set; }

        public List<Hashrate> HashRates { get; set; }
        //private List<GPU> Gpus { get; set; }
        public void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Model;
            this.Name = updateData.Name;
            this.Description = updateData.Description;

        }
    }
}