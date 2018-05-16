using System;
using System.Collections.Generic;
using System.Linq;

namespace CreaDev.Framework.Web.Seo.OpenGraph
{
    public class OpenGraphProperty
    {
        
        public string Name { get; set; }
        public string Value { get; set; }
        public string RenderHtmlElement()
        {
            return $@"<meta property=""{Name}"" content=""{Value}""/>"+Environment.NewLine;
        }
        public OpenGraphProperty(string name,string value)
        {
            
            this.Name = name;
            this.Value = value;

        }
    }
    public class OpenGraph
    {
        public OpenGraph(string title,string type,string url, List<string> images)
        {
            this.Title = new OpenGraphProperty(MetaDataPropertyNames.title, title);
            this.Type = new OpenGraphProperty(MetaDataPropertyNames.type, type);
            this.Url = new OpenGraphProperty(MetaDataPropertyNames.url, url);
            this.Images = images.Select(img=> new OpenGraphProperty(MetaDataPropertyNames.image, img)).ToList();
        }
        public OpenGraph(string title, string type, string url, string image,string siteName,string description) : this(title, type, url, new List<string> { image },siteName,description)
        {
            
        }
        public OpenGraph(string title, string type, string url, List<string> images, string siteName,string description):this(title,type,url,images)
        {
            this.Description = new OpenGraphProperty(MetaDataPropertyNames.description, description);
            this.SiteName = new OpenGraphProperty(MetaDataPropertyNames.siteName, siteName);
        }
        public OpenGraphProperty Title {get;set;}
        public OpenGraphProperty Type {get;set;}
        public OpenGraphProperty Url {get;set;}
        public List<OpenGraphProperty> Images { get;set;}
        public OpenGraphProperty Description { get;set; }
        public OpenGraphProperty SiteName { get;set; }
     
        public string RenderHtml()
        {

            string htmlResult = string.Empty;
            if(Title != null)
                htmlResult += Title.RenderHtmlElement();
            if (Type != null)
                htmlResult += Type.RenderHtmlElement();
            if (Url != null)
                htmlResult += Url.RenderHtmlElement();
            if (Images != null && Images.Any())
            {
                string imageElementsHtml = string.Empty;
                foreach (var item in Images)
                {
                    imageElementsHtml += item.RenderHtmlElement();
                }
                htmlResult += imageElementsHtml;

            }
            if (Description != null)
                htmlResult += Description.RenderHtmlElement();
            if (SiteName != null)
                htmlResult += SiteName.RenderHtmlElement();
            return htmlResult;
        }
    }
    public class MetaDataPropertyNames {
        public static string title = "og:title";
        public static string type = "og:type";
        public static string url = "og:url";
        public static string image = "og:image";
        public static string siteName = "og:site_name";
        public static string description = "og:description";
        
    }
    public static class OpenGraphType
    {
        public static string website="website";
        public static string article = "article";
        
        //
    }

}