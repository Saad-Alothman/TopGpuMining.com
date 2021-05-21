using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace CreaDev.Framework.Web.Mvc.Helpers
{
    public static class ControllerBaseExtension
    {
        public static string RenderPartialViewToString<TController>(this TController controller) where TController : ControllerBase
        {
            return RenderPartialViewToString(controller, null, null);
        }

        public static string RenderPartialViewToString<TController>(this TController controller, string viewName) where TController : ControllerBase
        {
            return RenderPartialViewToString(controller, viewName, null);
        }

        public static string RenderPartialViewToString<TController>(this TController controller, object model) where TController : ControllerBase
        {
            return RenderPartialViewToString(controller, null, model);
        }

        public static string RenderPartialViewToString<TController>(this TController controller, string viewName, object model, RouteValueDictionary viewDataVariables = null) where TController : ControllerBase
        {
            if (String.IsNullOrEmpty(viewName))
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");

            controller.ViewData.Model = model;
            if (viewDataVariables != null)
            {
                foreach (var viewDataVariable in viewDataVariables)
                {
                    controller.ViewData[viewDataVariable.Key] = viewDataVariable.Value;
                }
            }
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
    }

}
