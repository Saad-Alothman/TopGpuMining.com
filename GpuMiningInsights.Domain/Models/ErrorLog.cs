using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;
using CreaDev.Framework.Core.Extensions;
using CreaDev.Framework.Web.Mvc.Helpers;
using Microsoft.AspNet.Identity;

namespace GpuMiningInsights.Domain.Models
{
    public class ErrorLog : GmiEntityBase
    {
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string HttpRequest { get; set; }

        public string InnerExceptionErrorMessage { get; set; }
        public string InnerExceptionStackTrace { get; set; }
        public string MethodName { get; set; }
        public int? Line { get; set; }
        public string ParametersCsv { get; set; }
        public ErrorLog()
        {

        }

        public ErrorLog(Exception ex)
        {
            if (HttpContext.Current != null)
            {
                HttpRequest = HttpContext.Current?.Request?.ToRaw();
                List<string> parametersWithValues = HttpContext.Current?.Request.Params.AllKeys.Select(param => $"{param} => {HttpContext.Current.Request.Params[param]}").ToList();
                ParametersCsv = parametersWithValues.ToCsv();
            }
            Date = DateTime.Now;
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                string userId = Thread.CurrentPrincipal.Identity.GetUserId();
                UserId = userId;
            }

            ErrorMessage = ex.Message;
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);

            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();
            var methodName = frame.GetMethod().DeclaringType?.FullName + " =>" + frame.GetMethod().Name;
            this.Line = line;
            this.MethodName = methodName;
            StackTrace = ex.StackTrace;
            if (ex.InnerException != null)
            {
                InnerExceptionErrorMessage = ex.InnerException?.Message;
                InnerExceptionErrorMessage = ex.InnerException?.StackTrace;
            }
        }


        public override void Update(object objectWithNewData)
        {

        }
    }
}