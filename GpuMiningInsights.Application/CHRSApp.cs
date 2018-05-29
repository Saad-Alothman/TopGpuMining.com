using CreaDev.Framework.Core;
using GpuMiningInsights.Domain.Services;

namespace GpuMiningInsights.Application.Services
{
    public static class GmiApp
    {

        public static void Initialize()
        {
            InitializeServiceLocator();
        }
        private static void InitializeServiceLocator()
        {
            ServiceLocator.Add<IAlgorithmService>(AlgorithmService.Instance);
            ServiceLocator.Add<ICoinService>(CoinService.Instance);
        }
    }
}