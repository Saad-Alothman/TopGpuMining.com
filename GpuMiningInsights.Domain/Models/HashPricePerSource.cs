namespace GpuMiningInsights.Domain.Models
{
    public class HashPricePerSource
    {
        //Amazon, etc
        public string Source { get; set; }
        public double HashPrice { get; set; }
    }
}