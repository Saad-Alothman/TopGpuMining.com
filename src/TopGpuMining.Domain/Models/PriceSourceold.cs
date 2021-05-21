using System;
using System.Collections.Generic;
using System.Linq;
using TopGpuMining.Core;
using Newtonsoft.Json;

namespace TopGpuMining.Domain.Models
{
    public class ClientGpuListData
    {
        public ClientGpuListData()
        {

        }
        public ClientGpuListData(List<GPUOld> results, DateTime now)
        {
            this.Gpus= results;
            this.Date= now.ToString((string) AppSettings.DateFormat);
        }

        public List<GPUOld> Gpus { get; set; }
        public string Date { get; set; }
    }
    public class PriceSourceOld
    {
        public PriceSourceOld()
        {
            this.PriceSourceItems = new List<PriceSourceItemOld>();
        }
        public string Name { get; set; }
        public string URL { get; set; }
        public string Selector { get; set; }
        public string ImageUrl { get; set; }
        public string ImageUrlSelector { get; set; }
        public PriceSourceItemOld LowestPriceSourceItem
        {
            get
            {
                PriceSourceItemOld priceSourceItem = null;
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
        public List<PriceSourceItemOld> PriceSourceItems { get; set; }
        public bool RequiresJavascript { get; set; }
        [JsonIgnore]
        public Func<string, List<PriceSourceItemOld>> PriceSourceAction { get; set; }
        public string PriceSourceItemIdentifier { get; set; }
        public string ItemNameSelector { get; set; }

        public void AddPriceSourceItem(string price, string nameText, string currency)
        {
            PriceSourceItemOld priceSourceItem = new PriceSourceItemOld()
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
            PriceSourceItemOld priceSourceItem = new PriceSourceItemOld()
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

    public class PriceSourceItemOld
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