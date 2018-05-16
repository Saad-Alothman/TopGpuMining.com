using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap.Metronic.Portlet
{
    public static class PortletBodyExtension
    {

        public static PortletBody BeginPortletBody(this HtmlHelper htmlHelper)
        {
            return PortletBodyHelper(htmlHelper);
        }

        private static PortletBody PortletBodyHelper(this HtmlHelper htmlHelper)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("portlet-body");

            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            //

            PortletBody theForm = new PortletBody(htmlHelper.ViewContext);
            return theForm;
        }
    }
}