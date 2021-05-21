using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.TagHelpers
{
    [HtmlTargetElement("typeahead")]
    public class TypeaheadTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-text-for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-id-for")]
        public ModelExpression IdFor { get; set; }

        [HtmlAttributeName("asp-search-url")]
        public string SearchUrl { get; set; }

        [HtmlAttributeName("asp-template-id")]
        public string Template { get; set; } = "#default-typeahead-template";

        [HtmlAttributeName("asp-empty-template-id")]
        public string EmptyTemplate { get; set; }

        [HtmlAttributeName("asp-auto-submit")]
        public bool AutoSubmit { get; set; }

        [HtmlAttributeName("asp-placeholder")]
        public string Placeholder { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public TypeaheadTagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (For == null)
                throw new ArgumentException("Typeahead TagHelper must have asp-for value");


            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("class", "form-group");

            var labeTag = new TagBuilder("label");
            labeTag.Attributes.Add("for", For.Name);

            var displayName = For.Metadata?.DisplayName ?? For.Name;

            labeTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();

            SetAttributes(input);

            var hiddenFieldId = Guid.NewGuid().ToString().ToLower();

            var hiddenTag = GetHiddenTag(hiddenFieldId);

            if (hiddenTag != null)
            {
                input.Attributes.Add("data-id", hiddenFieldId);

                output.PostContent.AppendHtml(hiddenTag);
            }

            output.PreContent.AppendHtml(labeTag);

            output.PreContent.AppendHtml(input);


            base.Process(context, output);
        }

        private void SetAttributes(TagBuilder input)
        {
            if (!string.IsNullOrWhiteSpace(SearchUrl))
                input.Attributes.Add("data-search-url", SearchUrl);

            if (!string.IsNullOrWhiteSpace(Template))
                input.Attributes.Add("data-template-id", Template);

            if (!string.IsNullOrWhiteSpace(EmptyTemplate))
                input.Attributes.Add("data-empty-template-id", EmptyTemplate);

            input.Attributes.Add("data-auto-submit", AutoSubmit.ToString());

            input.AddCssClass("typeahead-auto");


        }

        private TagBuilder GetInputTag()
        {

            return Generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, "", new
            {
                @class = "form-control",
                autocomplete = "off",
                placeholder = Placeholder
            });

        }

        private TagBuilder GetHiddenTag(string id)
        {
            if (IdFor == null)
                return null;

            return Generator.GenerateHidden(ViewContext, IdFor.ModelExplorer, IdFor.Name, IdFor.Model, true, new
            {
                id,
                data_reset = "True"
            });


        }
    }
}
