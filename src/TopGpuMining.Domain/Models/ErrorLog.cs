using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;




namespace TopGpuMining.Domain.Models
{
    public class ErrorLog : BaseEntity
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
           
            Date = DateTime.Now;
            

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


        public void Update(object objectWithNewData)
        {

        }
    }
}