using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CreaDev.Framework.Core.Exceptions;
using CreaDev.Framework.Core.Resources;
using CreaDev.Framework.Web.Mvc.Helpers.CollectionVariableNames;
using CreaDev.Framework.Web.Mvc.Models;
using ValidationException = CreaDev.Framework.Core.Exceptions.ValidationException;

namespace CreaDev.Framework.Web.Mvc.Helpers
{
   public static class AlertExtension
    {
        public static Alert GetAlert<TModel>(TempDataDictionary tempData)
        {
            return (tempData[Ui.ALERT] as Alert);
        }
        public static Alert GetAlert(TempDataDictionary tempData)
        {
            return (tempData[Ui.ALERT] as Alert);
        }

        public static bool HasAlert<TModel>(TempDataDictionary tempData)
        {
            return (tempData[Ui.ALERT] as Alert) != null;
        }
        public static bool HasAlert(TempDataDictionary tempData)
        {
            return (tempData[Ui.ALERT] as Alert) != null;
        }
        public static bool HasAlert<TController>(this TController controller) where TController : ControllerBase
        {
            return (controller.TempData[Ui.ALERT] as Alert) != null;
        }
        public static Alert GetAlert<TController>(this TController controller) where TController : ControllerBase
        {
            return controller.TempData[Ui.ALERT] as Alert;

        }
        public static  void SetAlert<TController>(this TController controller,string message, Alert.Type type) where TController : ControllerBase
        {
            controller.SetAlert(new Alert(message, type));
        }
        public static  void SetAlert<TController>(this TController controller, string message, Alert.Type type, bool isAutoHide) where TController : ControllerBase
        {
            controller.SetAlert(new Alert(message, type, isAutoHide: isAutoHide));
        }
        public static  void SetAlert<TController>(this TController controller, Alert Alert) where TController : ControllerBase
        {
            controller.TempData[Ui.ALERT] = Alert;
        }
        public static  void SetSuccessAlert<TController>(this TController controller, string message = null) where TController : ControllerBase
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                message = Messages.OperationSuccess;
            }
            controller.TempData[Ui.ALERT] = new Alert(message, Alert.Type.Success);
        }
        public static  void SetFailedAlert<TController>(this TController controller, string message = null) where TController : ControllerBase
        {
            if (string.IsNullOrWhiteSpace(message))
                message = Messages.OperationFailed;

            if (controller.Request.IsAjaxRequest())
                controller.Response.SetBadRequest();

            controller.TempData[Ui.ALERT] = new Alert(message, Alert.Type.Error);
        }

        public static  void SetError<TController>(this TController controller, JsonResultObject result, bool isAutoHide = false, Exception ex = null) where TController : ControllerBase
        {
            controller.Response.SetBadRequest();
            result.Success = false;
            result.AlertMessage = GetErrorAlert(ex);
        }

        public static  void SetError<TController>(this TController controller, bool isAutoHide = false, Exception ex = null) where TController : ControllerBase
        {
            var alert = GetErrorAlert(ex);
            SetAlert(controller,alert);
        }

        public static  void SetSuccess<TController>(this TController controller, JsonResultObject result, bool isAutoHide = true) where TController : ControllerBase
        {
            result.AlertMessage = new Alert(Messages.OperationSuccess, Alert.Type.Success, isAutoHide);
        }

        public static  void SetSuccess<TController>(this TController controller, bool isAutoHide = true) where TController:ControllerBase
        {
            controller.SetAlert(new Alert(Messages.OperationSuccess, Alert.Type.Success, isAutoHide));
        }
        private static string GenerateErrorMessages(List<string> errors)
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append($"{Messages.PleaseFixTheFollowingErrors} : {Environment.NewLine}");
            sb.Append("<ul>");

            foreach (var item in errors)
                sb.Append($"<li>{item}</li>");

            sb.Append("</ul>");

            return sb.ToString();
        }
        private static string GenerateValidationExceptionMessage(ValidationException ex)
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append($"{Messages.PleaseFixTheFollowingErrors} : {Environment.NewLine}");
            sb.Append("<ul>");

            foreach (var item in ex.ValidationErrors)
                sb.Append($"<li>{item}</li>");

            sb.Append("</ul>");

            return sb.ToString();
        }

        private static Alert GetErrorAlert(Exception ex, bool isAutoHide = false)
        {
            Alert alert;

            if (ex != null && ex is ValidationException)
                alert = new Alert(GenerateValidationExceptionMessage(ex as ValidationException), Alert.Type.Error,
                    isAutoHide);
            else 
            //if (ex != null && ex is HRSqlException)
            //{
            //    var sqlException = ex as HRSqlException;

            //    alert = new Alert(sqlException.SqlExceptionMessage, Alert.Type.Error, isAutoHide);
            //}
            //else
            //if (ex != null && ex is LeaveException)
            //{
            //    var leaveException = ex as LeaveException;

            //    StringBuilder sb = new StringBuilder();

            //    sb.Append(ex.Message);
            //    sb.Append("<br/>");
            //    sb.Append("<br/>");


            //    foreach (var item in leaveException.Leaves.Select(a => a.LeaveRequest.ID).Distinct())
            //    {
            //        var leaveRequest =
            //            leaveException.Leaves.Where(a => a.LeaveRequest.ID == item)
            //                .Select(a => a.LeaveRequest)
            //                .FirstOrDefault();
            //        var leaveDays = leaveException.Leaves.Where(a => a.LeaveRequest.ID == item).ToList();

            //        sb.Append($"<p><strong>{Common.RequestNumber}:</strong> {leaveRequest.ID}</p>");

            //        sb.Append(
            //            $"<p><strong>{Core.Resources.Domain.LeaveRequest_StartDate}:</strong> {leaveRequest.StartDate?.ToSystemFormat()}</p>");
            //        sb.Append(
            //            $"<p><strong>{Core.Resources.Domain.LeaveRequest_EndDate}:</strong> {leaveRequest.EndDate?.ToSystemFormat()}</p>");

            //        sb.Append(
            //            $"<p><strong>{Core.Resources.Domain.LeaveRequest_Status}:</strong> {leaveRequest.Status}</p>");
            //    }


            //    alert = new Alert(sb.ToString(), Alert.Type.Error, isAutoHide);
            //}
            //else
                if (ex != null && ex is BusinessException)
            {
                var businessException = ex as BusinessException;

                if (businessException.Errors?.Count > 0)
                    alert = new Alert(GenerateErrorMessages(businessException.Errors), Alert.Type.Error, isAutoHide);
                else
                    alert = new Alert(ex.Message, Alert.Type.Error, isAutoHide);
            }
            else
                alert = new Alert(Messages.OperationFailed, Alert.Type.Error, isAutoHide);

            return alert;
        }
    }

}
