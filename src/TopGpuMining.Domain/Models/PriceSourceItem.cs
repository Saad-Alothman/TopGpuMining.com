namespace TopGpuMining.Domain.Models
{
    public class PriceSourceItem:BaseEntity
    {
        public PriceSourceItem()
        {
        }
        public PriceSource PriceSource { get; set; }
        public int? PriceSourceId { get; set; }
        public string ItemName { get; set; }
        public string ItemURL { get; set; }

        public string ASIN { get; set; }
        public string Ean { get; set; }

        public double Price { get; set; }
        public string PriceCurrency { get; set; }
        public double PriceUSD { get; set; }
        public string Merchant { get; set; }
        public string ImageUrl { get; set; }
        public string Model { get; set; }
        public string ModelYear { get; set; }
        public string Brand { get; set; }
        public string Manufacturer { get; set; }


    }
}