using System;
using System.Collections.Generic;
using Newtonsoft.Json;

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
        [JsonIgnore]
        public Func<string,List<PriceSourceItem>> PriceSourceAction { get; set; }
        public string PriceSourceItemIdentifier { get; set; }

        public void AddPriceSourceItem(string price)
        {
            PriceSourceItem priceSourceItem = new PriceSourceItem()
            {
                Name = Name,
                Price = double.Parse(price),
                Selector = Selector,
                
            };
            this.PriceSourceItems.Add(priceSourceItem);
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