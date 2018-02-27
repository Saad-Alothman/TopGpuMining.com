using GpuMiningInsights.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GpuMiningInsights.Web.Models
{
    public class GpuViewModel
    {
        public string Name { get; set; }
        public string WhatToMineUrl { get; set; }
        public List<PriceSource> PriceSources { get; set; }
        public PriceSource LowestPriceSource => PriceSources.OrderBy(p => p.PriceSourceItems.Min(a=>a.Price)).FirstOrDefault();
        public HashPricePerSource LowestHashPrice => HashPricePerSourceList.OrderBy(p => p.HashPrice).FirstOrDefault();
        public List<HashPricePerSource> HashPricePerSourceList { get; set; }
        //MHs/s
        public double Hashrate { get; set; }

        public GpuViewModel()
        {
            this.PriceSources = new List<PriceSource>();
            this.HashPricePerSourceList = new List<HashPricePerSource>();
        }

    }
}