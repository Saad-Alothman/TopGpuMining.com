using System.Collections.Generic;
using System.Linq;
using CreaDev.Framework.Core;
using CreaDev.Framework.Core.Linq.Expressions;

namespace GpuMiningInsights.Domain.Models
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