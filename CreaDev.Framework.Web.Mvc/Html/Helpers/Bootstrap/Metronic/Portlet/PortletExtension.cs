using System.Collections.Generic;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap.Metronic.Portlet
{
    public static class PortletExtension
    {

        public static Portlet BeginPortlet(this HtmlHelper htmlHelper,  ContainerColor containerColor ,PortletType portletType,bool isEqualHeight=false)
        {
            string additionalCssClass = isEqualHeight ? " equal-height":"";
            return PortletHelper(htmlHelper, new Dictionary<string, object>() { { "class", "portlet " + portletType.CssClass + " "+ containerColor.CssClass+ additionalCssClass} });
        }
      
        private static Portlet PortletHelper(this HtmlHelper htmlHelper,  IDictionary<string, object> htmlAttributes, params string[] anchors)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(htmlAttributes);

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            //

            Portlet theForm = new Portlet(htmlHelper.ViewContext);
            return theForm;
        }
    }
}