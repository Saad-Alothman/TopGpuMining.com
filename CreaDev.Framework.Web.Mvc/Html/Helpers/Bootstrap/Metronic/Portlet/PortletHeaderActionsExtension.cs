using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap.Metronic.Portlet
{
    public static class PortletHeaderActionsExtension
    {

        public static PortletHeaderActions BeginPortletHeaderActions(this HtmlHelper htmlHelper)
        {
            return PortletHeaderActionsHelper(htmlHelper);
        }

        private static PortletHeaderActions PortletHeaderActionsHelper(this HtmlHelper htmlHelper)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.AddCssClass("actions");
            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            //

            PortletHeaderActions theForm = new PortletHeaderActions(htmlHelper.ViewContext);
            return theForm;
        }
    }
}