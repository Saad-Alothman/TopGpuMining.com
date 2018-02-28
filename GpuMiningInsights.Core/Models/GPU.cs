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
        public PriceSource LowestPriceSource => PriceSources.OrderBy(p => p.PriceSourceItems.Min(m=>m.Price)).FirstOrDefault();
        public HashPricePerSource LowestHashPrice => HashPricePerSourceList.OrderBy(p => p.HashPrice).FirstOrDefault();
        public List<HashPricePerSource> HashPricePerSourceList { get; set; }
        //MHs/s
        public double Hashrate { get; set; }
        public string CoinToStudyName { get; set; }
        public MiningProfitability MiningProfitability { get; set; }
        public double RevenuePerDayUsd { get; set; }
        public double ProfitPerDayUsd { get; set; }
        public double ProfitPerYearMinusCostUsd => (ProfitPerDayUsd * 365) - LowestPriceSource.PriceSourceItems.Min(p=>p.Price);

        public GPU()
        {
            CoinToStudyName = "Ethereum(ETH)";
            this.MiningProfitability = new MiningProfitability();
            this.PriceSources = new List<PriceSource>();
            this.HashPricePerSourceList = new List<HashPricePerSource>();
        }

    }
}