namespace GpuMiningInsights.Domain.Models
{
    public class MiningProfitability
    {
        public string CryptoName { get; set; }
        public string CryptoAlgo { get; set; }
        public double Profitability24Hours { get; set; }
    }
}
