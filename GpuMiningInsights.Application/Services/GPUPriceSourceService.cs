using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Application.Services
{
    public class GpuPriceSourceService : GmiServiceBase<GPUPriceSource, GpuPriceSourceService>
    {
        public GpuPriceSourceService()
        {
            Includes.Add(nameof(GPUPriceSource.Gpu));
            Includes.Add(nameof(GPUPriceSource.PriceSource));
        }
    }
}