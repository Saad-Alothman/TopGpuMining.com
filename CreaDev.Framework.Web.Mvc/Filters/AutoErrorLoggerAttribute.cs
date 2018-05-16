using System;
using System.Net;
using System.Web.Mvc;

namespace CreaDev.Framework.Web.Mvc.Filters
{
    public class AutoErrorLoggerAttribute : ActionFilterAttribute,IExceptionFilter
    {
        bool _loglocal = true;
        public AutoErrorLoggerAttribute()
        {
            //Log = new Utils.CreadevLogger(typeof(AutoErrorLoggerAttribute));
        }
        public AutoErrorLoggerAttribute(Type type,bool logLocal=false)
        {
            //this.Log = new Utils.CreadevLogger(type);
            //this._loglocal = logLocal;
        }
        //public CreaDev.Framework.Utils.CreadevLogger Log;
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            //dont interfere if the exception is already handled
            if (filterContext.ExceptionHandled)
                return;

            //let the next request know what went wrong
            filterContext.Controller.TempData["exception"] = filterContext.Exception;
            if (_loglocal || !filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                //logg exception
          //      Log.Error(filterContext.Exception);

            }
            
            filterContext.ExceptionHandled = false;

        }
    }
}