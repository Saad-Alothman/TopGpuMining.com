using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace CreaDev.Framework.Web.Mvc.Html.Ajax
{
    public class CreaDevAjaxOptions: AjaxOptions
    {
        public CreaDevAjaxOptions()
        {
            
        }
        public CreaDevAjaxOptions(string targetElementSelector,string alertSelector,string modalToHideSelector, FormMethod httpMethod = FormMethod.Post)
        {
            this.HttpMethod = httpMethod.ToString();
            this.OnBegin = $"block('{targetElementSelector}');";
            this.OnFailure = $"onAjaxFailed(xhr, status, error, '{alertSelector}'); ";
            this.OnComplete = $"onAjaxComplete(xhr, status,'{targetElementSelector}','{alertSelector}','{targetElementSelector}');";
            this.OnSuccess = $"onAjaxSuccess(xhr,status,'{modalToHideSelector}');";

        }
        public CreaDevAjaxOptions(string elementToReplace,string elementToBlock, string alertElement="#alert", string modelToHide="",string onCompleteMethod=null ,string dataCallbackMethod=null, FormMethod httpMethod = FormMethod.Post)
        {
            string callbackMethod = !string.IsNullOrWhiteSpace(dataCallbackMethod) ? dataCallbackMethod : "null";
            this.HttpMethod = httpMethod.ToString();
            this.OnBegin = $"block('{elementToBlock}');";
            this.OnFailure = $"onAjaxFailed(xhr, status, error, '{alertElement}'); ";
            this.OnComplete = $"onAjaxComplete(xhr, status,'{elementToBlock}','{alertElement}','{elementToReplace}','null',{callbackMethod});{onCompleteMethod ?? ""}";
            this.OnSuccess = $"onAjaxSuccess(xhr,status,'{modelToHide}');";
            

        }


        public static MvcHtmlString GenerateAjaxUnobtrusiveHtmlAttributes(AjaxOptions options)
        {
            var attribues = options.ToUnobtrusiveHtmlAttributes();

            StringBuilder sb = new StringBuilder();

            foreach (var item in attribues)
            {
                sb.Append($"{item.Key}=\"{item.Value}\" ");
            }


            var result = MvcHtmlString.Create(sb.ToString());

            return result;
        }

    }
}
