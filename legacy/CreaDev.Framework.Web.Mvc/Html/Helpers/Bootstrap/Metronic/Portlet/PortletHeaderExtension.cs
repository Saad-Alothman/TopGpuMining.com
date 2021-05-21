using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap.Metronic.Portlet
{
    public static class PortletHeaderExtension
    {

        public static PortletHeader BeginPortletHeader(this HtmlHelper htmlHelper, string title = null, string iconCssClass = null)
        {
            return PortletHeaderHelper(htmlHelper, title, iconCssClass);
        }

        private static PortletHeader PortletHeaderHelper(this HtmlHelper htmlHelper, string title = "", string iconCssClass = "")
        {
            htmlHelper.ViewContext.Writer.Write($"<div class=\"portlet-title\">");
            if (title != null)
            {
                htmlHelper.ViewContext.Writer.Write($"<div class=\"caption\">");
                if (iconCssClass != null)
                {
                    htmlHelper.ViewContext.Writer.Write($"<i class=\"{iconCssClass}\"></i>");
                }
                
                htmlHelper.ViewContext.Writer.Write(title);
                htmlHelper.ViewContext.Writer.Write("</div>");
            }

            //

            PortletHeader theForm = new PortletHeader(htmlHelper.ViewContext);
            return theForm;
        }
    }
}