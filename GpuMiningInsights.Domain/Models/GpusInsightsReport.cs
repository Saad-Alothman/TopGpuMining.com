using GpuMiningInsights.Core.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class CoinProfitabilityResult
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

        [NotMapped]
        private List<CoinProfitabilityResult> _coinsProfitabilityResults { get; set; }

        [NotMapped]
        public List<CoinProfitabilityResult> CoinsProfitabilityResults
        {
            get
            {
                if (_coinsProfitabilityResults == null)
                {
                    CalculateCoinsProfitability();
                }
                return _coinsProfitabilityResults;
            }
        }

        private void CalculateCoinsProfitability()
        {
            List<CoinProfitabilityResult> coinsProfitabilityResults = new List<CoinProfitabilityResult>();
            /*
                                To Get HighestRevenueHash:

                                Get all Assigned Model_Hashrates (Hashrate Entity)
                                ForEach Model_Hashrates (Hashrate Entity)
                                    Get all coins with this hash
                                    ForEach Coin Calculate Profitability

                            In the End we will have Result of profitability based ON: Coin ,Profitability
                             */
            var coinService = ServiceLocator.Get<ICoinService>();
            foreach (var hashrate in Gpu.Model.HashRates)
            {
                var coinsWithThisAlgorithm = coinService.Search(new SearchCriteria<Coin>(c => c.AlgorithmId == hashrate.AlogrthimId),validCoinsOnly:true).Result;
                foreach (var coin in coinsWithThisAlgorithm)
                {
                    double revenuePerDayUsd = 0;
                    if (coin.ExchangeRateUsd.HasValue)
                        revenuePerDayUsd = CryptoUtils.CalculateCoinRevenuePerDay(double.Parse(hashrate.HashrateNumber),
                            coin.Difficulty, coin.BlockReward, coin.ExchangeRateUsd.Value);
                    else
                    {
                        double exchangeRate = coinService.GetLiveUsdExchangeRate(coin.Tag);
                        coinService.UpdateUsdExchangeRate(coin.Id,exchangeRate);
                        revenuePerDayUsd = CryptoUtils.CalculateCoinRevenuePerDay(double.Parse(hashrate.HashrateNumber), coin.Difficulty, coin.BlockReward, exchangeRate);
                    }

                    CoinProfitabilityResult coinProfitabilityResult = new CoinProfitabilityResult()
                    {
                        Hashrate = hashrate,
                        Coin = coin,
                        RevenuePerDay = revenuePerDayUsd
                    };
                    coinsProfitabilityResults.Add(coinProfitabilityResult);
                }



            }


            _coinsProfitabilityResults = coinsProfitabilityResults;
        }


        public CoinProfitabilityResult HighestRevenueCoin
        {
            get
            {
                CoinProfitabilityResult highestRevenueCoin = CoinsProfitabilityResults.Aggregate((i1, i2) => i1.RevenuePerDay > i2.RevenuePerDay ? i1 : i2);
                return highestRevenueCoin;
            }
        }
        public double ROI
        {
            get
            {
                //ROI = (Gain from Investment - Cost of Investment) / Cost of Investment


                double roi = ((HighestRevenueCoin.AnualRevenue - LowestPriceSourceItem.PriceUSD) / LowestPriceSourceItem.PriceUSD);
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

        public double ProfitPerDay
        {
            get
            {
                double profitPerDay = (HighestRevenueCoin.AnualRevenue - LowestPriceSourceItem.PriceUSD) / 365;
                return profitPerDay;
            }
        }

        public string CssStyle
        {
            get
            {
                string style = "";
                style = ROI >= 0 ? "" : "style=\"color:#de6c6c\"";
                if (ROI > 30)
                    style = "style=\"color:#0fc304\"";

                return style;
            }
        }



        public override void Update(object objectWithNewData)
        {
        }
    }
}
