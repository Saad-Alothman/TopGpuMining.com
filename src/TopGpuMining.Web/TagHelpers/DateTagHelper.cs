using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.TagHelpers
{
    [HtmlTargetElement("datetime-picker", TagStructure = TagStructure.WithoutEndTag)]
    public class DateTagHelper : TagHelper
    {
        public enum SelectMode
        {
            Date = 0,
            Month = 1,
            Year = 2,
            DateTime = 3
        }

        public enum ViewMode
        {
            Days = 0,
            Months = 1,
            Years = 2
        }

        [HtmlAttributeName("asp-id")]
        public string Id { get; set; }

        [HtmlAttributeName("asp-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-select-mode")]
        public SelectMode SelectModeType { get; set; } = SelectMode.Date;

        [HtmlAttributeName("asp-view-mode")]
        public ViewMode ViewModeType { get; set; } = ViewMode.Days;

        [HtmlAttributeName("asp-max-date")]
        public DateTime? MaxDate { get; set; }
        
        [HtmlAttributeName("asp-min-date")]
        public DateTime? MinDate { get; set; }

        [HtmlAttributeName("asp-view-date")]
        public DateTime? ViewDate { get; set; }

        [HtmlAttributeName("asp-hijri-default")]
        public bool IsHijriDefault { get; set; } = false;


        [HtmlAttributeName("asp-show-close")]
        public bool ShowClose { get; set; } = true;

        [HtmlAttributeName("asp-show-switch")]
        public bool ShowSwitch { get; set; } = true;

        [HtmlAttributeName("asp-show-today")]
        public bool ShowToday { get; set; } = true;

        [HtmlAttributeName("asp-show-clear")]
        public bool ShowClear { get; set; } = true;

        public string ViewModeValue
        {
            get
            {
                switch (ViewModeType)
                {
                    case ViewMode.Months:
                        return "months";
                    case ViewMode.Years:
                        return "years";
                    default:
                        return "days";
                }
            }
        }

        public string SelectModeValue
        {
            get
            {
                switch (SelectModeType)
                {
                    case SelectMode.Month:
                        return "month";
                    case SelectMode.Year:
                        return "year";
                    case SelectMode.DateTime:
                        return "date_time";
                    default:
                        return "day";
                }
            }
        }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public DateTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            
            var required = "";
            TagBuilder label = new TagBuilder("label");
            label.Attributes.Add("for", For.Name);

            var displayName = For.Metadata?.DisplayName ?? For.Name;

            if (For.Metadata.IsRequired)
                required = " <span class='required'>*</span>";
            

            label.InnerHtml.Append(displayName);
            label.InnerHtml.AppendHtml(required);

            TagBuilder dateDiv = GetDateDiveTag();
            TagBuilder validation = GetValidationTag();

            output.PreContent.AppendHtml(label);
            output.PostContent.AppendHtml(dateDiv);
            output.PostContent.AppendHtml(validation);
        }

        private void SetAttributes(TagBuilder tag)
        {
            if (MinDate.HasValue)
                tag.Attributes.Add("data-min-date", MinDate.Value.ToString("yyyy-MM-dd", new CultureInfo("en-US")));

            if (MaxDate.HasValue)
                tag.Attributes.Add("data-max-date", MaxDate.Value.ToString("yyyy-MM-dd", new CultureInfo("en-US")));

            if (ViewDate.HasValue)
                tag.Attributes.Add("data-view-date", ViewDate.Value.ToString("yyyy-MM-dd", new CultureInfo("en-US")));

            tag.Attributes.Add("data-default-hijri", IsHijriDefault.ToString().ToLower());
            tag.Attributes.Add("data-show-close", ShowClose.ToString().ToLower());
            tag.Attributes.Add("data-show-clear", ShowClear.ToString().ToLower());
            tag.Attributes.Add("data-show-today", ShowToday.ToString().ToLower());
            tag.Attributes.Add("data-show-switch", ShowSwitch.ToString().ToLower());

            tag.Attributes.Add("data-select-mode", SelectModeValue);
            tag.Attributes.Add("data-view-mode", ViewModeValue);
            
        }

        private TagBuilder GetDateDiveTag()
        {
            TagBuilder dateDiv = new TagBuilder("div");
            dateDiv.AddCssClass("input-group");
            dateDiv.TagRenderMode = TagRenderMode.Normal;

            var input = GetInputTag();
            var icon = GetIconTag();

            dateDiv.InnerHtml.AppendHtml(input);
            dateDiv.InnerHtml.AppendHtml(icon);
            
            return dateDiv;
        }

        private TagBuilder GetIconTag()
        {
            TagBuilder inputGroup = new TagBuilder("div");
            inputGroup.TagRenderMode = TagRenderMode.Normal;
            inputGroup.AddCssClass("input-group-append");

            TagBuilder div = new TagBuilder("div");
            div.AddCssClass("input-group-text");
            div.TagRenderMode = TagRenderMode.Normal;

            TagBuilder icon = new TagBuilder("i");
            icon.AddCssClass("fa fa-calendar");

            div.InnerHtml.AppendHtml(icon);
            
            inputGroup.InnerHtml.AppendHtml(div);

            return inputGroup;
        }

        private TagBuilder GetInputTag()
        {

            var format = "{0:dd-MM-yyyy}";

            if (SelectModeType == SelectMode.DateTime)
                format = "{0:dd-MM-yyyy HH:mm tt}";

            if (SelectModeType == SelectMode.Month)
                format = "{0:MM-yyyy}";

            var  result = Generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.ModelExplorer.Model, format, new
            {
                @class = "form-control hijri-date-picker",
                @id = !string.IsNullOrWhiteSpace(Id) ? Id : For.Name,
                autocomplete = "off"
            });

            SetAttributes(result);

            return result;

        }

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, null, null, new
            {
                @class = "text-danger"
            });
        }
    }
}
