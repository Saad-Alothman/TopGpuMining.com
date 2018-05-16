using System.Collections.Generic;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    public static class RowExtension
    {
        public static Row BeginRow(this HtmlHelper htmlHelper)
        {
            return RowHelper(htmlHelper, new Dictionary<string, object>() { { "class", "row" } });
        }
        private static Row RowHelper(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(htmlAttributes);




            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            Row theForm = new Row(htmlHelper.ViewContext);



            return theForm;
        }

    }
}