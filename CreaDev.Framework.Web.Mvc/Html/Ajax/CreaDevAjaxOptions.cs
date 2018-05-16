using System.Web.Mvc.Ajax;

namespace CreaDev.Framework.Web.Mvc.Html.Ajax
{
    public class CreaDevAjaxOptions: AjaxOptions
    {
        public CreaDevAjaxOptions()
        {
            
        }
        public CreaDevAjaxOptions(string httpMethod,string targetElementSelector,string alertSelector,string modalToHideSelector)
        {
            this.HttpMethod = httpMethod;
            this.OnBegin = $"block('{targetElementSelector}');";
            this.OnFailure = $"onAjaxFailed(xhr, status, error, '{alertSelector}'); ";
            this.OnComplete = $"onAjaxComplete(xhr, status,'{targetElementSelector}','{alertSelector}','{targetElementSelector}');";
            this.OnSuccess = $"onAjaxSuccess(xhr,status,'{modalToHideSelector}');";

        }
        public CreaDevAjaxOptions(string httpMethod, string elementToReplace,string elementToBlock, string alertElement, string modelToHide,string onScuccessMethod=null ,string dataCallbackMethod=null)
        {
            string callbackMethod = !string.IsNullOrWhiteSpace(dataCallbackMethod) ? dataCallbackMethod : "null";
            this.HttpMethod = httpMethod;
            this.OnBegin = $"block('{elementToBlock}');";
            this.OnFailure = $"onAjaxFailed(xhr, status, error, '{alertElement}'); ";
            this.OnComplete = $"onAjaxComplete(xhr, status,'{elementToBlock}','{alertElement}','{elementToReplace}','null',{callbackMethod});{onScuccessMethod ?? ""}";
            this.OnSuccess = $"onAjaxSuccess(xhr,status,'{modelToHide}');";
            

        }
        
    }
}
