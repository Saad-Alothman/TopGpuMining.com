using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using TopGpuMining.Core;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Helpers;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Services;

namespace TopGpuMining.Domain.Models
{
    public class GpusInsightsReport : BaseEntity
    {
        public DateTime Date { get; set; }
        public List<GpuInsightReport> GpuInsightReports { get; set; }

        public void Update(object objectWithNewData)
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
        public double CoinsPerDay { get; set; }

        public double CalcProfitPerDay(double gpuPrice)
        {
            return (AnualRevenue - gpuPrice) / 365;
        }
    }
    public class GpuInsightReport : BaseEntity
    {

        public GpuInsightReport()
        {
            this.PriceSourceItems = new List<PriceSourceItem>();
        }
        public Gpu Gpu { get; set; }
        public string GpuId { get; set; }
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
        private Dictionary<string, List<Coin>> _coincsByAlgorithmCache = new Dictionary<string, List<Coin>>();
        private List<Coin> GetCoiubsByAlgorithmId(string algorithmId)
        {
            if (algorithmId == null)
            {
                throw new ArgumentNullException();
            }

            List<Coin> result = null;
            if (_coincsByAlgorithmCache.ContainsKey(algorithmId))

                result = _coincsByAlgorithmCache[algorithmId];
            else
            {
                result = ServiceLocator.Current.GetService<ICoinService>()
                    .Search(new SearchCriteria<Coin>(c => c.AlgorithmId == algorithmId), validCoinsOnly: true).Result;
                _coincsByAlgorithmCache.Add(algorithmId,result);
            }
            return result;
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
            var coinService = ServiceLocator.Current.GetService<ICoinService>();
            foreach (var hashrate in Gpu.Model.HashRates)
            {
                var coinsWithThisAlgorithm = GetCoiubsByAlgorithmId(hashrate.AlogrthimId);
                foreach (var coin in coinsWithThisAlgorithm)
                {
                    double revenuePerDayUsd = 0;
                    double coinsPerDay = CryptoUtils.CalculateCoinRevenuePerDayByNethashAndBlockTime(double.Parse(coin.BlockTime), coin.BlockReward, (coin.Nethash / 1000 / 1000), double.Parse(hashrate.HashrateValueMhz));
                    if (coin.ExchangeRateUsd.HasValue)
                    {
                        revenuePerDayUsd = coinsPerDay * coin.ExchangeRateUsd.Value;
                    }

                    else
                    {
                        double exchangeRate = coinService.GetLiveUsdExchangeRate(coin.Tag);
                        if (exchangeRate ==0)
                        {
                            continue;
                        }
                        else
                        {
                            coinService.UpdateUsdExchangeRate(coin.Id, exchangeRate);
                            revenuePerDayUsd = coinsPerDay * exchangeRate;

                        }
                    }

                    CoinProfitabilityResult coinProfitabilityResult = new CoinProfitabilityResult()
                    {
                        Hashrate = hashrate,
                        Coin = coin,
                        RevenuePerDay = revenuePerDayUsd,
                        CoinsPerDay = coinsPerDay
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
                CoinProfitabilityResult highestRevenueCoin = null;
                if (CoinsProfitabilityResults.Any())
                    highestRevenueCoin = CoinsProfitabilityResults.Aggregate((i1, i2) => i1.RevenuePerDay > i2.RevenuePerDay ? i1 : i2);
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
                        lowest = item;
                }
                return lowest;
            }
        }

        public double ProfitPerDay
        {
            get
            {
                double profitPerDay = HighestRevenueCoin.CalcProfitPerDay(LowestPriceSourceItem.PriceUSD);

                return profitPerDay;
            }
        }

        public string CssStyle
        {
            get { return GetRoiCssStyle(ROI); }
        }

        public static string GetRoiCssStyle(double roi)
        {
            string style = "";
            style = roi >= 0 ? "" : "style=\"color:#de6c6c\"";
            if (roi > 30)
                style = "style=\"color:#0fc304\"";

            return style;
        }


        public void Update(object objectWithNewData)
        {
        }
    }
}
