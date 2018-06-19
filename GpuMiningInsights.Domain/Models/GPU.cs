using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CreaDev.Framework.Core;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Core.Resources;

namespace GpuMiningInsights.Domain.Models
{
    public class Gpu : GmiEntityBase
    {
        public Brand Brand { get; set; }
        public int? BrandId { get; set; }

        public Model Model { get; set; }
        public int? ModelId { get; set; }

        [Display(Name = nameof(Common.Name), ResourceType = typeof(Common))]
        public string Name { get; set; }

        [Display(Name = nameof(Common.Description), ResourceType = typeof(Common))]

        public string Description { get; set; }

        public string Asin { get; set; }
        public string Ean { get; set; }
        public string ImageUrl { get; set; }

        public List<GPUPriceSource> GPUPriceSources { get; set; }

        public override void Update(object objectWithNewData)
        {
            var updateData = objectWithNewData as Gpu;
            this.Name = updateData.Name;
            this.Description = updateData.Description;
            this.BrandId = updateData.BrandId;
            this.ModelId = updateData.ModelId;
            this.Asin = updateData.Asin;
            this.Ean = updateData.Ean;
            this.ImageUrl = updateData.ImageUrl;

        }

        public override void ValidateAdd()
        {
            
        }
    }
}