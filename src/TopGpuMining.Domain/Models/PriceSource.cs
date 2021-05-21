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
    public  class PriceSource : BaseEntity
    {
        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        public string Name { get; set; }

        [Display(Name = nameof(CommonText.Url), ResourceType = typeof(CommonText))]
        public string Url { get; set; }
        [Display(Name = nameof(CommonText.PriceSourceType), ResourceType = typeof(CommonText))]
        public PriceSourceType PriceSourceType { get; set; }

        public bool IsScrape = false;
        public bool RequiresJavascript = false;


        public void Update(PriceSource updateData)
        {
            this.Name = updateData.Name;
            this.Url = updateData.Url;
            this.IsScrape = updateData.IsScrape;
            this.RequiresJavascript = updateData.RequiresJavascript;
            this.PriceSourceType = updateData.PriceSourceType;
        }
    }
}