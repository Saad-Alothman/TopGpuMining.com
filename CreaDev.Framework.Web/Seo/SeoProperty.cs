namespace CreaDev.Framework.Web.Seo
{
   public abstract class SeoProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public abstract string RenderHtmlElement();
      
        public SeoProperty(string name, string value)
        {

            this.Name = name;
            this.Value = value;

        }
    }
}
