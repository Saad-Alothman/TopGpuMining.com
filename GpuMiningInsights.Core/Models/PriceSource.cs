using System;
using System.Collections.Generic;

namespace GpuMiningInsights.Core
{
    public class PriceSource
    {
        public PriceSource()
        {
            this.PriceSourceItems = new List<PriceSourceItem>();
        }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Selector { get; set; }
        public List<PriceSourceItem> PriceSourceItems { get; set; }
        public bool RequiresJavascript { get; set; }
        public Func<string,List<PriceSourceItem>> PriceSourceAction { get; set; }
        public string PriceSourceItemIdentifier { get; set; }

        public void AddPriceSourceItem(string price)
        {
            PriceSourceItem priceSourceItems = new PriceSourceItem()
            {
                Name = Name,
                Price = double.Parse(price),
                Selector = Selector,
                
            };
        }
    }

    public class PriceSourceItem
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public string Selector { get; set; }
        public double Price { get; set; }
        public string ASIN { get; set; }
        public string PriceCurrency { get; set; }
        public string Merchant { get; set; }
    }
}