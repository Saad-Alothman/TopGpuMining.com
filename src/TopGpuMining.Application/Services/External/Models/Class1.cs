using System;
using Newtonsoft.Json;

namespace TopGpuMining.Application.Services.External.Models
{
    

    public partial class AmazonBuddyProductResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("feature_bullets")]
        public string[] FeatureBullets { get; set; }

        [JsonProperty("variants")]
        public object[] Variants { get; set; }

        [JsonProperty("categories")]
        public Category[] Categories { get; set; }

        [JsonProperty("asin")]
        public string Asin { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("reviews")]
        public Reviews Reviews { get; set; }

        [JsonProperty("item_available")]
        public bool ItemAvailable { get; set; }

        [JsonProperty("price")]
        public Price Price { get; set; }

        [JsonProperty("bestsellers_rank")]
        public BestsellersRank[] BestsellersRank { get; set; }

        [JsonProperty("main_image")]
        public Uri MainImage { get; set; }

        [JsonProperty("total_images")]
        public long TotalImages { get; set; }

        [JsonProperty("images")]
        public Uri[] Images { get; set; }

        [JsonProperty("total_videos")]
        public long TotalVideos { get; set; }

        [JsonProperty("videos")]
        public Video[] Videos { get; set; }

        [JsonProperty("delivery_message")]
        public string DeliveryMessage { get; set; }

        [JsonProperty("product_information")]
        public ProductInformation ProductInformation { get; set; }

        [JsonProperty("badges")]
        public Badges Badges { get; set; }

        [JsonProperty("sponsored_products")]
        public object[] SponsoredProducts { get; set; }

        [JsonProperty("also_bought")]
        public object[] AlsoBought { get; set; }

        [JsonProperty("other_sellers")]
        public object[] OtherSellers { get; set; }
    }

    public partial class Badges
    {
        [JsonProperty("amazon_сhoice")]
        public bool AmazonСhoice { get; set; }

        [JsonProperty("amazon_prime")]
        public bool AmazonPrime { get; set; }
    }

    public partial class BestsellersRank
    {
        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }
    }

    public partial class Category
    {
        [JsonProperty("category")]
        public string CategoryCategory { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }

    public partial class Price
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("current_price")]
        public long CurrentPrice { get; set; }

        [JsonProperty("discounted")]
        public bool Discounted { get; set; }

        [JsonProperty("before_price")]
        public long BeforePrice { get; set; }

        [JsonProperty("savings_amount")]
        public long SavingsAmount { get; set; }

        [JsonProperty("savings_percent")]
        public long SavingsPercent { get; set; }
    }

    public partial class ProductInformation
    {
        [JsonProperty("dimensions")]
        public string Dimensions { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("available_from")]
        public string AvailableFrom { get; set; }

        [JsonProperty("available_from_utc")]
        public DateTimeOffset AvailableFromUtc { get; set; }

        [JsonProperty("available_for_months")]
        public long AvailableForMonths { get; set; }

        [JsonProperty("available_for_days")]
        public long AvailableForDays { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("model_number")]
        public string ModelNumber { get; set; }

        [JsonProperty("department")]
        public string Department { get; set; }

        [JsonProperty("qty_per_order")]
        public string QtyPerOrder { get; set; }

        [JsonProperty("store_id")]
        public string StoreId { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }
    }

    public partial class Reviews
    {
        [JsonProperty("total_reviews")]
        public long TotalReviews { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }

        [JsonProperty("answered_questions")]
        public long AnsweredQuestions { get; set; }
    }

    public partial class Video
    {
        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("thumb")]
        public Uri Thumb { get; set; }

        [JsonProperty("durationSeconds")]
        public long DurationSeconds { get; set; }

        [JsonProperty("marketPlaceID")]
        public string MarketPlaceId { get; set; }

        [JsonProperty("isVideo")]
        public bool IsVideo { get; set; }

        [JsonProperty("isHeroVideo")]
        public bool IsHeroVideo { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("languageCode")]
        public string LanguageCode { get; set; }

        [JsonProperty("holderId")]
        public string HolderId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("videoHeight")]
        public long VideoHeight { get; set; }

        [JsonProperty("videoWidth")]
        public long VideoWidth { get; set; }

        [JsonProperty("durationTimestamp")]
        public string DurationTimestamp { get; set; }

        [JsonProperty("slateUrl")]
        public Uri SlateUrl { get; set; }

        [JsonProperty("minimumAge")]
        public long MinimumAge { get; set; }

        [JsonProperty("variant")]
        public string Variant { get; set; }

        [JsonProperty("slateHash")]
        public SlateHash SlateHash { get; set; }

        [JsonProperty("mediaObjectId")]
        public string MediaObjectId { get; set; }

        [JsonProperty("thumbUrl")]
        public Uri ThumbUrl { get; set; }
    }

    public partial class SlateHash
    {
        [JsonProperty("extension")]
        public string Extension { get; set; }

        [JsonProperty("physicalID")]
        public object PhysicalId { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }
    }
}
