using TopGpuMining.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.TagHelpers
{

    [HtmlTargetElement("bootstrap-input-form-group")]
    public class BootstrapInputFormTagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }
        
        [HtmlAttributeName("asp-placeholder")]
        public string Placeholder { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public BootstrapInputFormTagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var required = "";
            if (For.Metadata.IsRequired)
                required = " <span class='required'>*</span>";


            var labeTag = new TagBuilder("label");
            labeTag.Attributes.Add("for", For.Name);

            var displayName = For.Metadata?.DisplayName ?? For.Name;
            displayName += required;

            labeTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();
            var validationTag = GetValidationTag();

            output.PreContent.AppendHtml(labeTag);
            output.PreContent.AppendHtml(input);
            output.PreContent.AppendHtml(validationTag);

            base.Process(context, output);
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

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, "", null, new
            {
                @class = "text-danger"
            });
        }
    }

    [HtmlTargetElement("bootstrap-password-form-group")]
    public class BootstrapPasswordFormTagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public BootstrapPasswordFormTagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var required = "";
            if (For.Metadata.IsRequired)
                required = " <span class='required'>*</span>";

            var labeTag = new TagBuilder("label");
            labeTag.Attributes.Add("for",For.Name);

            var displayName = For.Metadata?.DisplayName ?? For.Name;

            displayName += required;

            labeTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();
            var validationTag = GetValidationTag();

            output.PreContent.AppendHtml(labeTag);
            output.PreContent.AppendHtml(input);
            output.PreContent.AppendHtml(validationTag);

            base.Process(context, output);
        }

        private TagBuilder GetInputTag()
        {

            return Generator.GeneratePassword(ViewContext, For.ModelExplorer, For.Name, For.Model, new
            {
                @class = "form-control",
                autocomplete = "off"
            });

        }

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, "", null, new
            {
                @class = "text-danger"
            });
        }
    }

    [HtmlTargetElement("bootstrap-mobile-form-group")]
    public class BootstrapMobileFormTagHelper : TagHelper
    {
        [HtmlAttributeName("for")]
        public ModelExpression For { get; set; }

        [HtmlAttributeName("asp-placeholder")]
        public string Placeholder { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected IHtmlGenerator Generator { get; }

        public BootstrapMobileFormTagHelper(IHtmlGenerator htmlGenerator)
        {
            Generator = htmlGenerator;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", "form-group");

            var required = "";
            if (For.Metadata.IsRequired)
                required = " <span class='required'>*</span>";


            var labeTag = new TagBuilder("label");
            labeTag.Attributes.Add("for", For.Name);

            var displayName = For.Metadata?.DisplayName ?? For.Name;
            displayName += required;

            labeTag.InnerHtml.AppendHtml(displayName);

            var input = GetInputTag();
            var validationTag = GetValidationTag();

            var inputGroup = GetInputGroup();
            var inputGroupPrepend = GetInputGroupPrepend();
            var inputGroupText = GetInputGroupText();
        
            inputGroupPrepend.InnerHtml.AppendHtml(inputGroupText);

            if (Language.IsEnglish)
            {
                inputGroup.InnerHtml.AppendHtml(inputGroupPrepend);
                inputGroup.InnerHtml.AppendHtml(input);
            }
            else
            {
                inputGroup.InnerHtml.AppendHtml(input);
                inputGroup.InnerHtml.AppendHtml(inputGroupPrepend);
            }
            

            output.PreContent.AppendHtml(labeTag);
            output.PreContent.AppendHtml(inputGroup);
            output.PreContent.AppendHtml(validationTag);

            base.Process(context, output);
        }

        private TagBuilder GetInputTag()
        {

            return Generator.GenerateTextBox(ViewContext, For.ModelExplorer, For.Name, For.Model, "", new
            {
                @class = "form-control",
                dir = "ltr",
                autocomplete = "off",
                placeholder = Placeholder
            });

        }

        private TagBuilder GetValidationTag()
        {
            return Generator.GenerateValidationMessage(ViewContext, For.ModelExplorer, For.Name, "", null, new
            {
                @class = "text-danger"
            });
        }

        private TagBuilder GetInputGroup()
        {
            var result = new TagBuilder("div");
            result.AddCssClass("input-group");
            return result;
        }

        private TagBuilder GetInputGroupPrepend()
        {
            var result = new TagBuilder("div");

            if (Language.IsEnglish)
                result.AddCssClass("input-group-prepend");
            else
                result.AddCssClass("input-group-append");

            result.Attributes.Add("dir", "ltr");

            return result;
        }
        
        private TagBuilder GetInputGroupText()
        {
            var result = new TagBuilder("span");

            result.AddCssClass("input-group-text");

            result.InnerHtml.Append("+966");

            return result;
        }
    }
}
