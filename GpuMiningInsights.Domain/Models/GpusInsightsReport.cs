using GpuMiningInsights.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Domain.Models
{
    public class GpusInsightsReport : GmiEntityBase
    {
        public DateTime Date { get; set; }
        public List<GpuInsightReport> GpuInsightReports { get; set; }

        public override void Update(object objectWithNewData)
        {

        }
        public GpusInsightsReport()
        {
            this.Date = DateTime.Now;
            this.GpuInsightReports = new List<GpuInsightReport>();
        }
    }

    public class GpuInsightReport:GmiEntityBase
    {
        public GpuInsightReport()
        {
            this.PriceSourceItems = new List<PriceSourceItem>();
        }
        public Gpu Gpu { get; set; }
        public List<PriceSourceItem> PriceSourceItems { get; set; }
        public Hashrate HighestRevenueHash
        {
            get
            { 
                Hashrate lowest = Gpu.Model.HashRates.FirstOrDefault();
                double highestRev = Gpu.Model.HashRates.FirstOrDefault();
                foreach (var hashrate in Gpu.Model.HashRates)
                {
                  double revenuePerDay=  CryptoUtils.CalculateCoinRevenuePerDay(Gpu.Model)
                        if (revenuePerDay > hi)
                    {

                    }


                }
            }
        }
        public double AnnualRevenue
        {
            get
            {
                //Loop over all Hashrates and find the highest Revenue Per Day
                CryptoUtils.CalculateCoinRevenuePerDay(Gpu.Model)
                return ProfitPerDayUsd * 365;
            }
        }
        public PriceSourceItem LowestPriceSourceItem { get
            {
                PriceSourceItem lowest = PriceSourceItems.FirstOrDefault();
                foreach (var item in PriceSourceItems)
                {
                    if (item.PriceUSD < lowest.PriceUSD)
                    {
                        lowest = item;
                    }
                }
                return lowest;
            }
        }

        public override void Update(object objectWithNewData)
        {
        }
    }
}
