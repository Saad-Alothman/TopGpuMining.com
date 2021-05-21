using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Resources;

namespace TopGpuMining.Domain.Models
{
    public class Gpu : BaseEntity
    {
        public Brand Brand { get; set; }
        public string BrandId { get; set; }

        public Model Model { get; set; }
        public string ModelId { get; set; }

        [Display(Name = nameof(CommonText.Name), ResourceType = typeof(CommonText))]
        public string Name { get; set; }

        [Display(Name = nameof(CommonText.Description), ResourceType = typeof(CommonText))]

        public string Description { get; set; }

        public string Asin { get; set; }
        public string Ean { get; set; }
        public string ImageUrl { get; set; }

        public List<GpuPriceSource> GPUPriceSources { get; set; }

        public void Update(object objectWithNewData)
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


    }
}