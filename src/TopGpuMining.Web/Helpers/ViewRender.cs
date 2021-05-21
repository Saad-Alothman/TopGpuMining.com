using TopGpuMining.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TopGpuMining.Web.Helpers
{
    public class ViewRender : IViewRender
    {
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHttpContextAccessor _httpContext;

        public ViewRender(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IHttpContextAccessor httpContext,
            IServiceProvider serviceProvider)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            _httpContext = httpContext;
        }

        public async Task<string> RenderAsync(string viewName, object model, object viewData = null)
        {
            var httpContext = _httpContext.HttpContext;
            var routeData = httpContext.GetRouteData();

            var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.GetView(null, viewName, false);

                if (!viewResult.Success)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                ViewDataDictionary viewDataDictionay;

                if (viewData == null)
                {
                    viewDataDictionay = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = model
                    };
                }
                else
                {
                    viewDataDictionay = viewData as ViewDataDictionary;

                    viewDataDictionay.Model = model;
                }

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDataDictionay,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return sw.ToString();
            }
        }

    }
}
