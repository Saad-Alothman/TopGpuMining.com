using System;

namespace CreaDev.Framework.Web.Seo
{
    public class MetaTagProperty: SeoProperty
    {
        public string nameAttribute = "name";
        public string valueAttribute = "content";
        public override string RenderHtmlElement()
        {
            return $@"<meta {nameAttribute}=""{Name}"" {valueAttribute}=""{Value}""/>" + Environment.NewLine;
        }
        public MetaTagProperty(string name, string value):base(name,value)
        {


        }
        public MetaTagProperty(string name, string value,string nameAttribute,string valueAttribute) : this(name, value)
        {
            this.nameAttribute = nameAttribute;
            this.valueAttribute = valueAttribute;

        }
    }
}
