using System.Web;
using System.Web.Mvc;
using CreaDev.Framework.Core;

namespace CreaDev.Framework.Web.Mvc.Filters
{
    public class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, System.Reflection.MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
    public class LoggedInUserUpdateAttribute : ActionFilterAttribute
    {
        private string _sessionVariableName;

        public LoggedInUserUpdateAttribute(string sessionVariableName)
        {
            this._sessionVariableName = sessionVariableName;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session == null)
                return;

            if (!string.IsNullOrWhiteSpace(_sessionVariableName))
            {
                filterContext.HttpContext.Session[_sessionVariableName] = null;
            }
            else
            {
                filterContext.HttpContext.Session.Clear();

            }
        }
    }
}
