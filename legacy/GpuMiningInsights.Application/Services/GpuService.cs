using System.Collections.Generic;
using System.Linq;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Application.Services
{
    public class GpuService : GmiServiceBase<Gpu, GpuService>
    {
        public GpuService()
        {
            Includes = new System.Collections.Generic.List<string>()
            {
                nameof(Gpu.GPUPriceSources),
                $"{nameof(Gpu.GPUPriceSources)}.{nameof(GPUPriceSource.PriceSource)}",
                nameof(Gpu.Model),
            };
        }

        public override void Add(List<Gpu> models)
        {
            List<string> asins = models.Select(s => s.Asin).ToList();
            var existing =Search(new SearchCriteria<Gpu>(int.MaxValue, 1)
            {
                FilterExpression = g => asins.Contains(g.Asin)
            }).Result;
            List<Gpu> nonExisting = models.Where(m=> existing.All(e => e.Asin != m.Asin)).ToList();
            base.Add(nonExisting);
        }
    }
}