using System.Collections.Generic;
using System.Linq;



namespace TopGpuMining.Domain.Models
{
    public class CurrencyPricesResponse
    {
        public bool success { get; set; }
        public string terms { get; set; }
        public string privacy { get; set; }
        public int timestamp { get; set; }
        public string source { get; set; }
        public Dictionary<string, double> quotes { get; set; }
    }
}