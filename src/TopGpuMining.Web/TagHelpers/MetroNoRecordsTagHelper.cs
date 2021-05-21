using TopGpuMining.Core.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.TagHelpers
{
    [HtmlTargetElement("metro-no-records")]
    public class MetroNoRecordsTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("class", "alert alert-outline-primary fade show");

            TagBuilder iconDiv = new TagBuilder("div");
            iconDiv.AddCssClass("alert-icon");

            TagBuilder icon = new TagBuilder("i");
            icon.AddCssClass("flaticon-warning");

            iconDiv.InnerHtml.AppendHtml(icon);

            TagBuilder alertText = new TagBuilder("div");
            alertText.AddCssClass("alert-text");
            alertText.InnerHtml.Append(MessageText.NoRecords);

            output.Content.AppendHtml(iconDiv);
            output.Content.AppendHtml(alertText);

            base.Process(context, output);
        }
    }
}
