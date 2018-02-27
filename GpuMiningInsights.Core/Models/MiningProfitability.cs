using System;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Core
{
    public class MiningProfitability
    {
        public string CryptoName { get; set; }
        public string CryptoAlgo { get; set; }
        public double Profitability24Hours { get; set; }
    }
}
