using System;
using System.Collections.Generic;
using System.Linq;
using GpuMiningInsights.Core;
using Newtonsoft.Json;

namespace GpuMiningInsights.Domain.Models
{
    public class ClientGpuListData
    {
        public ClientGpuListData()
        {

        }
        public ClientGpuListData(List<GPUOld> results, DateTime now)
        {
            this.Gpus= results;
            this.Date= now.ToString((string) Settings.DateFormat);
        }

        public List<GPUOld> Gpus { get; set; }
        public string Date { get; set; }
    }
    public class PriceSource
    {
        public PriceSource()
        {
            this.PriceSourceItems = new List<PriceSourceItem>();
        }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Selector { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlSelector { get; set; }
        public PriceSourceItem LowestPriceSourceItem
        {
            get
            {
                PriceSourceItem priceSourceItem = null;
                if (PriceSourceItems == null || !PriceSourceItems.Any())
                {
                    return priceSourceItem;
                }


                var nonEmptyUsdItems = PriceSourceItems.Where(s => s.PriceUSD > 0);
                if (nonEmptyUsdItems != null && nonEmptyUsdItems.Any())
                {
                    priceSourceItem = nonEmptyUsdItems.FirstOrDefault();
                    foreach (var item in nonEmptyUsdItems)
                    {
                        if (item.PriceUSD < priceSourceItem.PriceUSD)
                        {
                            priceSourceItem = item;
                        }
                    }
                }
                return priceSourceItem;
            }
        }
        public List<PriceSourceItem> PriceSourceItems { get; set; }
        public bool RequiresJavascript { get; set; }
        [JsonIgnore]
        public Func<string, List<PriceSourceItem>> PriceSourceAction { get; set; }
        public string PriceSourceItemIdentifier { get; set; }
        public string ItemNameSelector { get; set; }

        public void AddPriceSourceItem(string price, string nameText, string currency)
        {
            PriceSourceItem priceSourceItem = new PriceSourceItem()
            {
                Name = nameText,
                Price = double.Parse(price),
                Selector = Selector,
                PriceCurrency = currency
            };
            this.PriceSourceItems.Add(priceSourceItem);
        }
        public void AddPriceSourceItem(string price,string nameText, string currency, string imageUrl)
        {
            PriceSourceItem priceSourceItem = new PriceSourceItem()
            {
                Name = nameText,
                Price = double.Parse(price),
                Selector = Selector,
                ImageUrl = imageUrl,
                PriceCurrency = currency,
                
                
                

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
        public string ImageUrl { get; set; }
        public double PriceUSD { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string Brand { get; set; }
        public string Manufacturer { get; set; }
        public string Ean { get; set; }
    }
}