using GpuMiningInsights.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CreaDev.Framework.Core;
using CreaDev.Framework.Core.Models;
using GpuMiningInsights.Domain.Services;

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
    public class HighestRevenueHashResult
    {
        public Hashrate Hashrate { get; set; }
        public Coin Coin { get; set; }
        public double RevenuePerDay { get; set; }
        public double AnualRevenue => RevenuePerDay * 365;

    }
    public class GpuInsightReport : GmiEntityBase
    {

        public GpuInsightReport()
        {
            this.PriceSourceItems = new List<PriceSourceItem>();
        }
        public Gpu Gpu { get; set; }
        public List<PriceSourceItem> PriceSourceItems { get; set; }
        private HighestRevenueHashResult _highestRevenueHash;
        public HighestRevenueHashResult HighestRevenueHash
        {
            get
            {
                if (_highestRevenueHash == null)
                {
                    var coinService = ServiceLocator.Get<ICoinService>();
                    List<Hashrate> hashrates = Gpu.Model.HashRates;
                    Hashrate highetRevenueHash = hashrates.FirstOrDefault();
                    var hcoin = coinService
                        .Search(new SearchCriteria<Coin>(c => c.AlgorithmId == highetRevenueHash.AlogrthimId)).Result
                        .FirstOrDefault();
                    double highestRevPerDay = CryptoUtils.CalculateCoinRevenuePerDay(double.Parse(highetRevenueHash.HashrateNumber), hcoin.Difficulty, hcoin.BlockReward);
                    foreach (var hashrate in Gpu.Model.HashRates)
                    {
                        var currentCoin = coinService
                            .Search(new SearchCriteria<Coin>(c => c.AlgorithmId == hashrate.AlogrthimId)).Result
                            .FirstOrDefault();
                        double revenuePerDay = CryptoUtils.CalculateCoinRevenuePerDay(double.Parse(hashrate.HashrateNumber), currentCoin.Difficulty, currentCoin.BlockReward);

                        if (revenuePerDay > highestRevPerDay)
                        {
                            highetRevenueHash = hashrate;
                            hcoin = currentCoin;
                            highestRevPerDay = revenuePerDay;
                        }


                    }
                    HighestRevenueHashResult highestRevenueHashResult = new HighestRevenueHashResult()
                    {
                        Hashrate = highetRevenueHash,
                        Coin = hcoin,
                        RevenuePerDay = highestRevPerDay
                    };
                    _highestRevenueHash = highestRevenueHashResult;
                }

                return _highestRevenueHash;


            }
        }
        public double ROI
        {
            get
            {
                //ROI = (Gain from Investment - Cost of Investment) / Cost of Investment


                double roi = ((HighestRevenueHash.AnualRevenue - LowestPriceSourceItem.PriceUSD) / LowestPriceSourceItem.PriceUSD);
                roi = Math.Round(roi * 100, 2);
                return roi;
            }
        }

        public PriceSourceItem LowestPriceSourceItem
        {
            get
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

        public double ProfitPerDay {
            get
            {
                double profitPerDay = (HighestRevenueHash.AnualRevenue - LowestPriceSourceItem.PriceUSD) / 365;
                return profitPerDay;
            }
        }

        public override void Update(object objectWithNewData)
        {
        }
    }
}
