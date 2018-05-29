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
                $"{nameof(Gpu.GPUPriceSources)}.{nameof(GPUPriceSource.PriceSource)}"
            };
        }
    }
}