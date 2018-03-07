using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GpuMiningInsights.Core
{
    [Serializable]
    public class GPU
    {
        public string Name { get; set; }
        public string WhatToMineUrl { get; set; }
        public List<PriceSource> PriceSources { get; set; }

        public PriceSource LowestPriceSource
        {
            get
            {
                PriceSource lowestPriceSource = null;
                var priceSourcesWithPriceSourceItems = PriceSources.Where(s => s.PriceSourceItems.Any()).ToList();
                if (priceSourcesWithPriceSourceItems.Any())
                    lowestPriceSource = priceSourcesWithPriceSourceItems.OrderBy(p => p.PriceSourceItems.Min(m => m.Price)).FirstOrDefault();

                return lowestPriceSource;
            }
        }


        public HashPricePerSource LowestHashPrice => HashPricePerSourceList.OrderBy(p => p.HashPrice).FirstOrDefault();
        public List<HashPricePerSource> HashPricePerSourceList { get; set; }
        //MHs/s
        public double Hashrate { get; set; }
        public string CoinToStudyName { get; set; }
        public MiningProfitability MiningProfitability { get; set; }
        public double RevenuePerDayUsd { get; set; }
        public double ProfitPerDayUsd { get; set; }
        [JsonIgnore]
        public double? ProfitPerYearMinusCostUsd {
            get
            {
                double? profitPerYearMinusCostUsd = null;
                if (LowestPriceSource!= null && LowestPriceSource.PriceSourceItems != null && LowestPriceSource.PriceSourceItems.Any())
                    profitPerYearMinusCostUsd = (ProfitPerDayUsd * 365) - (LowestPriceSource.PriceSourceItems.Min(p => p.PriceUSD));
                return profitPerYearMinusCostUsd;
            }
        }

        public GPU()
        {
            CoinToStudyName = "Ethereum(ETH)";
            this.MiningProfitability = new MiningProfitability();
            this.PriceSources = new List<PriceSource>();
            this.HashPricePerSourceList = new List<HashPricePerSource>();
        }

    }


    
 
}