using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Application.Services
{
    public interface IAlgorithmService: IGmiServiceBase<Algorithm> 
    {
        Algorithm AddOrUpdate(Algorithm model);

    }
}