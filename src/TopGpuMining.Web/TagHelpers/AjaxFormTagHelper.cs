using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.TagHelpers
{
    /*
    | Confirm                | data-ajax-confirm           |
    | HttpMethod             | data-ajax-method            |
    | InsertionMode          | data-ajax-mode              |
    | LoadingElementDuration | data-ajax-loading-duration  |
    | LoadingElementId       | data-ajax-loading           |
    | OnBegin                | data-ajax-begin             |
    | OnComplete             | data-ajax-complete          |
    | OnFailure              | data-ajax-failure           |
    | OnSuccess              | data-ajax-success           |
    | UpdateTargetId         | data-ajax-update            |
    | Url                    | data-ajax-url 
    */

    [HtmlTargetElement("form", Attributes = "asp-ajax")]
    [HtmlTargetElement("button", Attributes = "asp-ajax")]
    [HtmlTargetElement("a", Attributes = "asp-ajax")]
    public class AjaxFormTagHelper : TagHelper
    {
        [HtmlAttributeName("asp-ajax")]
        public bool EnableAjax { get; set; }

        [HtmlAttributeName("asp-ajax-block")]
        public string Block { get; set; }

        [HtmlAttributeName("asp-ajax-success-method")]
        public string SuccessMethod { get; set; }

        [HtmlAttributeName("asp-ajax-complete-method")]
        public string CompleteMethod { get; set; }

        [HtmlAttributeName("asp-ajax-replace")]
        public string Replace { get; set; } = "";

        [HtmlAttributeName("asp-ajax-error")]
        public string Error { get; set; }

        [HtmlAttributeName("asp-ajax-modal")]
        public string Modal { get; set; }

        [HtmlAttributeName("asp-form-id")]
        public string FormId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (EnableAjax)
            {
                var errorDiv = "";

                if (!string.IsNullOrWhiteSpace(Error))
                    errorDiv = Error;

                output.Attributes.Add("data-ajax", "true");
                output.Attributes.Add("data-ajax-begin", $"block('{Block}')");

                if (string.IsNullOrWhiteSpace(FormId))
                    FormId = output.Attributes["id"] != null ? "#" + output.Attributes["id"].Value : null;
                
                output.Attributes.Add("data-ajax-failure", $"onAjaxFailed(xhr,status,error,'{errorDiv}','{FormId}');");

                if (!string.IsNullOrWhiteSpace(CompleteMethod))
                    output.Attributes.Add("data-ajax-complete", $"onAjaxComplete(xhr, status, '{Block}', '{errorDiv}', '{Replace}', '{FormId}');{CompleteMethod};");

                else
                    output.Attributes.Add("data-ajax-complete", $"onAjaxComplete(xhr, status, '{Block}', '{errorDiv}', '{Replace}', '{FormId}');");

                if (!string.IsNullOrWhiteSpace(SuccessMethod))
                    output.Attributes.Add("data-ajax-success", $"onAjaxSuccess(xhr, status, '{Modal}');{SuccessMethod};");
                else
                    output.Attributes.Add("data-ajax-success", $"onAjaxSuccess(xhr, status, '{Modal}')");



            }
        }
    }
}
