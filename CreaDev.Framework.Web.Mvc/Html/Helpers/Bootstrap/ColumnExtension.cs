using System.Collections.Generic;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Html.Helpers.Bootstrap
{
    public static class ColumnExtension
    {
        public static Column BeginColumn(this HtmlHelper htmlHelper,int columnWidth=12)
        {
            return ColumnHelper(htmlHelper, new Dictionary<string, object>() { { "class", $"col-md-{columnWidth}" } });
        }
        private static Column ColumnHelper(this HtmlHelper htmlHelper, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder tagBuilder = new TagBuilder("div");
            tagBuilder.MergeAttributes(htmlAttributes);




            htmlHelper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            Column theForm = new Column(htmlHelper.ViewContext);



            return theForm;
        }

    }
}