using CreaDev.Framework.Core.Models;

namespace CreaDev.Framework.Web.Seo.TwitterCard
{
    public class TwitterCard : StructuredDataMarkupAbstract
    {

        public TwitterCardType TwitterCardType { get; set; }
        public MetaTagProperty Card { get; set; }
        public MetaTagProperty Site { get; set; }
        public MetaTagProperty Title { get; set; }
        public MetaTagProperty Description { get; set; }
        public MetaTagProperty Image { get; set; }

        public TwitterCard(TwitterCardType type, string site, string title, string description)
        {
            this.TwitterCardType = type;
            this.Card = new MetaTagProperty(MetaDataPropertyNames.card, type.DisplayName);
            this.Site = new MetaTagProperty(MetaDataPropertyNames.site, site);
            this.Title = new MetaTagProperty(MetaDataPropertyNames.title, title);
            this.Description = new MetaTagProperty(MetaDataPropertyNames.description, description);
        }
        public TwitterCard(TwitterCardType type, string site, string title, string description, string image) : this(type, site, title, description)
        {
            if(!string.IsNullOrWhiteSpace(image))
            this.Image = new MetaTagProperty(MetaDataPropertyNames.image, image);
        }

        public override string RenderHtml()
        {
            string resultHtml = string.Empty;
            resultHtml += Card.RenderHtmlElement();
            resultHtml += Site.RenderHtmlElement();
            resultHtml += Title.RenderHtmlElement();
            resultHtml += Description.RenderHtmlElement();
            if (Image != null)
                resultHtml += Image.RenderHtmlElement();

            return resultHtml;
        }
    }
    public static class MetaDataPropertyNames
    {
        public static string card = "twitter:card";
        public static string site = "twitter:site";
        public static string title = "twitter:title";
        public static string description = "twitter:description";
        public static string image = "twitter:image";
    }
    public class TwitterCardType : Enumeration
    {
        public static readonly TwitterCardType Summary
         = new TwitterCardType(1, "summary");
        public static readonly TwitterCardType SummaryWithLargeImage
            = new TwitterCardType(2, "summary_large_image");


        private TwitterCardType() { }
        private TwitterCardType(int value, string displayName) : base(value, displayName) { }
    }


}
