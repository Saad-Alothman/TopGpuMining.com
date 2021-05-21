using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CreaDev.Framework.Core.Exceptions;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Controllers;
using NotFoundException = OpenQA.Selenium.NotFoundException;

namespace GpuMiningInsights.Web.Models.Filters
{
    public class ExceptionHandlerFilter : HandleErrorAttribute, IExceptionFilter
    {
        public override void OnException(ExceptionContext filterContext)
        {
            bool isSendEmail = true;
#if DEBUG
            isSendEmail = false;
#endif
            //going to rearange the exception if it is dbException
            if (filterContext.Exception is DbEntityValidationException)
                HandleDbEntityValidationException(filterContext);

            var errorLog = new ErrorLog(filterContext.Exception);
            GenericService<ErrorLog>.Instance.Add(errorLog);
            //ErrorLogService.Instance.Add(errorLog, isSendEmail);

            if (filterContext.Exception is PermissionException)
                HandlePermissionException(filterContext, errorLog);

            else if (filterContext.Exception is NotFoundException)
                HandleNotFoundException(filterContext, errorLog);
            else
                HandleException(filterContext, errorLog);

            //            if (filterContext.HttpContext.Request.IsAjaxRequest())
            bool isHandeled = (filterContext.HttpContext.Request.IsLocal &&
                               filterContext.HttpContext.Request.IsAjaxRequest()) ||
                              !filterContext.HttpContext.Request.IsLocal;
            filterContext.ExceptionHandled = isHandeled;
            base.OnException(filterContext);
        }

        private static void HandleException(ExceptionContext filterContext, ErrorLog errorLog)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JsonResultObject result = new JsonResultObject();
                result.Success = false;
                string message = ("Common.WeAreFixing" + " , #" + errorLog.Id.ToString("00000"));
                if (filterContext.HttpContext.Request.IsLocal)
                {
                    message = filterContext.Exception.Message + Environment.NewLine + "st: " +
                              (filterContext.Exception.StackTrace ?? "");
                }
                result.AlertMessage = new Alert(message, Alert.Type.Error);

                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                filterContext.Result = new JsonDotNetResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = result
                };
                filterContext.HttpContext.Items["ErrorNumber"] = errorLog.Id.ToString("00000");
            }
            else
            {
                var routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Error";
                routeValues["Action"] = "InternalServerError";
                routeValues["id"] = errorLog.Id.ToString("00000");
                //TODO: routeValues["message"] = result.AlertMessage;
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
        }
        private static void HandleNotFoundException(ExceptionContext filterContext, ErrorLog errorLog)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                JsonResultObject result = new JsonResultObject();
                result.Success = false;
                result.AlertMessage = new Alert("Common.NotFound" + " , #" + errorLog.Id.ToString("00000"), Alert.Type.Error);

                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.Result = new JsonDotNetResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = result
                };
                filterContext.HttpContext.Items["ErrorNumber"] = errorLog.Id.ToString("00000");
            }
            else
            {
                var routeValues = new RouteValueDictionary();
                routeValues["controller"] = "Error";
                routeValues["Action"] = "NotFound";
                routeValues["id"] = errorLog.Id.ToString("00000");
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
        }

        private static void HandleDbEntityValidationException(ExceptionContext filterContext)
        {
            var exception = new Exception();
            string entitiesValidationErrors = "";
            DbEntityValidationException dbEntityValidationException = (DbEntityValidationException)filterContext.Exception;
            foreach (var entityValidationError in dbEntityValidationException.EntityValidationErrors)
            {

                string entityValidationErrors = "";
                foreach (var dbValidationError in entityValidationError.ValidationErrors)
                {
                    entityValidationErrors += dbValidationError.PropertyName + ":" + dbValidationError.ErrorMessage + " " + Environment.NewLine;
                }

                entitiesValidationErrors += entityValidationErrors + " " + Environment.NewLine;
            }

            exception = new Exception(entitiesValidationErrors, dbEntityValidationException);

            filterContext.Exception = exception;
        }
        private static void HandlePermissionException(ExceptionContext filterContext, ErrorLog errorLog)
        {
            filterContext.ExceptionHandled = true;

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var routeValues = new RouteValueDictionary();

                routeValues["controller"] = "PermissionDenied";
                routeValues["Action"] = "Index";
                routeValues["id"] = errorLog.Id.ToString("00000");
                filterContext.Result = new RedirectToRouteResult(routeValues);
            }
            else
            {
                JsonResultObject result = new JsonResultObject();
                result.Success = false;
                result.AlertMessage = new Alert("Messages.PermissionDenied", Alert.Type.Error);

                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                filterContext.Result = new JsonDotNetResult()
                {
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    Data = result
                };
            }
        }

    }

}