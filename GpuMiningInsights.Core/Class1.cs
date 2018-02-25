using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpuMiningInsights.Core
{
    public class GPU
    {
        public string Name { get; set; }
        public string WhatToMineUrl { get; set; }
        public List<PriceSource> PriceSources { get; set; }
        public PriceSource LowestPriceSource => PriceSources.OrderBy(p => p.Price).FirstOrDefault();
        public HashPricePerSource LowestHashPrice => HashPricePerSourceList.OrderBy(p => p.HashPrice).FirstOrDefault();
        public List<HashPricePerSource> HashPricePerSourceList { get; set; }
        //MHs/s
        public double Hashrate { get; set; }
        public string CoinToStudyName { get; set; }
        public MiningProfitability MiningProfitability { get; set; }
        public double RevenuePerDayUsd { get; set; }
        public double ProfitPerDayUsd { get; set; }
        public double ProfitPerYearMinusCostUsd => (ProfitPerDayUsd * 365) - LowestPriceSource.Price;

        public GPU()
        {
            CoinToStudyName = "Ethereum(ETH)";
            this.MiningProfitability = new MiningProfitability();
            this.PriceSources = new List<PriceSource>();
            this.HashPricePerSourceList = new List<HashPricePerSource>();
        }

    }
    public class HashPricePerSource
    {
        //Amazon, etc
        public string Source { get; set; }
        public double HashPrice { get; set; }
    }
    public class MiningProfitability
    {
        public string CryptoName { get; set; }
        public string CryptoAlgo { get; set; }
        public double Profitability24Hours { get; set; }
    }
    public class PriceSource
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Selector { get; set; }
        public double Price { get; set; }
        public bool RequiresJavascript { get; set; }
    }
}
