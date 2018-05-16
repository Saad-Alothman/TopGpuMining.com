using System.Web;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Seo.RichSnippets
{
    //public class MicroDataAttributeModel {
    //    public string Name { get; set; }
    //    public string Value { get; set; }
    //    public override string ToString()
    //    {
    //        return $@"{MicroDataModel.itemProp}=""{}"" ";
    //    }
    //}
    public class ProductMicroDataHelper
    {
        public static string ItemScope { get { return @" itemscope itemtype=""http://schema.org/Product"" "; } }
        public static string NameAttribute = @"itemprop=""name"" ";
        public static string ImageAttribute = @"itemprop=""image"" ";
        public static string DescriptionAttribute = @"itemprop=""description"" ";
        public static string BrandAttribute = @"itemprop=""brand"" ";
        public static string SkuAttribute = @"itemprop=""sku"" ";
        public HtmlHelper htmlHelper;
        public ProductMicroDataHelper(HtmlHelper htmlHelper)
        {
            this.htmlHelper = htmlHelper;
        }
        public  IHtmlString Name
        {
         get { return htmlHelper.Raw(ProductMicroDataHelper.NameAttribute); }
        }
        public IHtmlString Image
        {
            get { return htmlHelper.Raw(ProductMicroDataHelper.ImageAttribute); }
        }
        public IHtmlString Description
        {
            get { return htmlHelper.Raw(ProductMicroDataHelper.DescriptionAttribute); }
        }
        public IHtmlString Brand
        {
            get { return htmlHelper.Raw(ProductMicroDataHelper.BrandAttribute); }
        }
        public IHtmlString Sku
        {
            get { return htmlHelper.Raw(ProductMicroDataHelper.SkuAttribute); }
        }
   
    }

}