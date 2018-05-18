using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using CreaDev.Framework.Core;
using CreaDev.Framework.Core.Exceptions;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Core.Resources;
using CreaDev.Framework.Web.Mvc;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Filters;

namespace GpuMiningInsights.Web.Controllers
{
    //[Authorize]
    [ExceptionHandlerFilter]
    public class BaseController : Controller
    {



        public BaseController()
        {
        }

        protected string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        protected string RenderPartialViewToString(string viewName, object model,
            RouteValueDictionary viewDataVariables = null)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;
            if (viewDataVariables != null)
            {
                foreach (var viewDataVariable in viewDataVariables)
                {
                    ViewData[viewDataVariable.Key] = viewDataVariable.Value;
                }
            }
            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected void SetBadRequest()
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }

        protected void SetAlert(string message, Alert.Type type)
        {
            SetAlert(new Alert(message, type));
        }

        protected void SetAlert(string message, Alert.Type type, bool isAutoHide)
        {
            SetAlert(new Alert(message, type, isAutoHide: isAutoHide));
        }

        protected void SetAlert(Alert Alert)
        {
            TempData[Constants.ALERT] = Alert;
        }

        protected void SetError(JsonResultObject result, bool isAutoHide = false, Exception ex = null)
        {
            SetBadRequest();
            result.Success = false;
            result.AlertMessage = GetErrorAlert(ex);
        }

        protected void SetError(bool isAutoHide = false, Exception ex = null)
        {
            var alert = GetErrorAlert(ex);
            SetAlert(alert);
        }

        protected void SetSuccess(JsonResultObject result, bool isAutoHide = true)
        {
            result.AlertMessage = new Alert(Messages.OperationSuccess, Alert.Type.Success, isAutoHide);
        }

        protected void SetSuccess(bool isAutoHide = true)
        {
            SetAlert(new Alert(Messages.OperationSuccess, Alert.Type.Success, isAutoHide));
        }


        private Alert GetErrorAlert(Exception ex, bool isAutoHide = false)
        {
            Alert alert;


            alert = new Alert(Messages.OperationFailed, Alert.Type.Error, isAutoHide);

            return alert;
        }

        protected void ThrowModelStateErrors()
        {
            List<string> errors = new List<string>();


            var validationErrors = ModelState.Where(a => a.Value.Errors.Count > 0).Select(
                p => new
                {
                    Key = p.Key,
                    Erros = p.Value.Errors.Select(a => a.ErrorMessage).ToArray()
                }).ToList();



            foreach (var item in validationErrors)
            {
                foreach (var error in item.Erros)
                {
                    errors.Add(error);
                }

            }

            throw new ValidationException(errors);
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding,
            JsonRequestBehavior behavior)
        {
            return new JsonDotNetResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };

        }

        private string GenerateValidationExceptionMessage(ValidationException ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Messages.PleaseFixTheFollowingErrors} : {Environment.NewLine}");
            sb.Append("<ul>");

            foreach (var item in ex.ValidationErrors)
                sb.Append($"<li>{item}</li>");

            sb.Append("</ul>");

            return sb.ToString();
        }

        private string GenerateErrorMessages(List<string> errors)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Messages.PleaseFixTheFollowingErrors} : {Environment.NewLine}");
            sb.Append("<ul>");

            foreach (var item in errors)
                sb.Append($"<li>{item}</li>");

            sb.Append("</ul>");

            return sb.ToString();
        }

        private string GetStaticForm(string label, string info)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"form-group\">");
            sb.Append("<label class=\"label-control\">");
            sb.Append(label);
            sb.Append("</label>");
            sb.Append("<br/>");
            sb.Append("<p class=\"form-control-static\">");
            sb.Append(info);
            sb.Append("</p>");
            sb.Append("</div>");

            return sb.ToString();
        }


        protected new JsonResult Json(object data)
        {
            return new JsonDotNetResult
            {
                Data = data,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = JsonRequestBehavior.DenyGet
            };

        }
        protected new JsonResult JsonGet(object data)
        {
            return new JsonDotNetResult
            {
                Data = data,
                ContentType = null,
                ContentEncoding = null,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }



        public ActionResult SimpleAjaxAction(Action action, bool setSuccessMessage = true)
        {
            JsonResultObject result = new JsonResultObject();
            try
            {
                action.Invoke();
                if (setSuccessMessage)
                    SetSuccess(result);

            }
            catch (BusinessException ex)
            {
                SetError(result, false, ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        protected ActionResult SimpleUpdateAction(Action action, string viewName = "")
        {
            try
            {
                action.Invoke();
                SetSuccess();
            }
            catch (BusinessException ex)
            {
                SetError(false, ex);
            }
            return RedirectToAction("Index");
        }


        protected ActionResult SimpleAction(Action action, Func<ActionResult> actionResult, bool isSetSuccess = true)
        {
            try
            {
                action.Invoke();
                if (isSetSuccess)
                    SetSuccess();

            }
            catch (BusinessException ex)
            {
                SetError(false, ex);
            }
            return actionResult.Invoke();
        }
        protected ActionResult SimpleDetailsAction<TModel>(Func<TModel> action, string viewName = "")
        {
            try
            {
                var model = action.Invoke();
                SetSuccess();
                if (!string.IsNullOrWhiteSpace(viewName))
                    return View(viewName, model);
                return View(model);
            }
            catch (BusinessException ex)
            {
                SetError(false, ex);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// use this when that is return method of the action is the object  to pass to the View
        /// </summary>
        /// <param name="action">Action to Call</param>
        /// <param name="partialViewName">Partial View Name</param>
        /// <param name="setSuccess">Should this display a success message when success?</param>
        /// <returns></returns>
        public ActionResult AjaxAction(Func<object> action, string partialViewName, bool setSuccess = true)
        {
            JsonResultObject result = new JsonResultObject();
            try
            {
                var resultObj = action.Invoke();
                result.PartialViewHtml = RenderPartialViewToString(partialViewName, resultObj);

                if (setSuccess)
                    SetSuccess(result);

            }
            catch (BusinessException ex)
            {
                SetError(result, false, ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// use this when you need to call another method to get the ViewModel
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="action">Action to Call</param>
        /// <param name="actionForVeiwModel">the Action that will return the viewmodel to be passed</param>
        /// <param name="partialViewName">Partial View Name</param>
        /// <param name="setSuccess">Should this display a success message when success?</param>

        /// <returns></returns>
        protected ActionResult AjaxAction<TViewModel>(Action action, Func<TViewModel> actionForVeiwModel, string partialViewName, bool setSuccess)
        {
            JsonResultObject result = new JsonResultObject();
            try
            {
                action.Invoke();
                var resultObj = actionForVeiwModel.Invoke();
                result.PartialViewHtml = RenderPartialViewToString(partialViewName, resultObj);

                if (setSuccess)
                    SetSuccess(result);

            }
            catch (BusinessException ex)
            {
                SetError(result, false, ex);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }



        //TODO: To be Continued
        protected ActionResult SimpleSearchAjaxAction<T, TSearchCriteriaViewModel>(TSearchCriteriaViewModel model,
            string listPartialViewName, Func<SearchCriteria<T>, SearchResult<T>> action)
            where TSearchCriteriaViewModel : SearchCriteriaViewModelBase<T>, new()
            where T : GmiEntityBase
        {
            if (model == null)
            {
                model = new TSearchCriteriaViewModel() { PageNumber = 1, PageSize = 10 };
            }
            ViewData[Constants.SEARCH_MODEL] = model;
            var searchCriteria = model.ToSearchCriteria();
            SearchResult<T> modelToView = action.Invoke(searchCriteria);

            if (Request.IsAjaxRequest())
                return PartialView(listPartialViewName, modelToView);

            return View(modelToView);
        }
 

        internal void AddViewDataSearchModel<TSearchViewModel>(TSearchViewModel tSearchViewModel)
        {
            ViewData[Constants.SEARCH_MODEL] = tSearchViewModel;
            Dictionary<Type, object> dictionary = ViewData[Constants.SEARCH_MODEL_DICTIONARY] as Dictionary<Type, object> ?? new Dictionary<Type, object>();
            var key = typeof(TSearchViewModel);
            if (dictionary.ContainsKey(key))
                dictionary[key] = tSearchViewModel;
            else
                dictionary.Add(key, tSearchViewModel);
            ViewData[Constants.SEARCH_MODEL_DICTIONARY] = dictionary;
        }

        protected ActionResult DetailsEdit<TModel, TIdType>(TIdType id, Func<TIdType, TModel> actionToGetModel, string partialViewName)
        {
            JsonResultObject result = new JsonResultObject();
            TModel model = actionToGetModel.Invoke(id);
            result.PartialViewHtml = RenderPartialViewToString(partialViewName, model);
            return Json(result);
        }
        protected ActionResult DetailsUpdate<TModel>(TModel model, Action<TModel> actionToUpdate, string partialViewName) where TModel : GmiEntityBase
        {
            JsonResultObject result = new JsonResultObject();

            try
            {
                actionToUpdate.Invoke(model);
                SetSuccess(result);
            }
            catch (BusinessException ex)
            {
                SetError(result, false, ex);
            }
            result.PartialViewHtml = RenderPartialViewToString(partialViewName, model);
            return Json(result);
        }

        protected ActionResult DetailsGet<TModel>(int id, Func<int, TModel> action, string partialViewName, bool isJsonResultObject = false)
            where TModel : GmiEntityBase
        {

            JsonResultObject result = new JsonResultObject();
            TModel model = null;
            try
            {
                model = action(id);

            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }
            if (isJsonResultObject)
            {
                result.PartialViewHtml = RenderPartialViewToString(partialViewName, model);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(partialViewName, model);
            }



        }
        protected ActionResult DetailsGetList<TModel>(int id, Func<int, List<TModel>> action, string partialViewName, bool isJsonResultObject = false)
            where TModel : GmiEntityBase
        {

            JsonResultObject result = new JsonResultObject();
            List<TModel> model = new List<TModel>();
            try
            {
                model = action(id);

            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }
            if (isJsonResultObject)
            {
                result.PartialViewHtml = RenderPartialViewToString(partialViewName, model);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return PartialView(partialViewName, model);
            }



        }
        protected ActionResult DetailsAdd<TModel>(TModel model, Action<TModel> actionToAdd) where TModel : GmiEntityBase
        {
            JsonResultObject result = new JsonResultObject();
            try
            {
                actionToAdd(model);
                SetSuccess(result);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }
            return Json(result);
        }


        #region NewMethods

        protected ActionResult SimpleIndex<TModel>(Func<SearchCriteria<TModel>, SearchResult<TModel>> action)
            where TModel : GmiEntityBase
        {
            SearchResult<TModel> viewModel = new SearchResult<TModel>();
            try
            {
                SearchCriteria<TModel> searchCriteria = new SearchCriteria<TModel>();
                viewModel = action.Invoke(searchCriteria);
            }
            catch (Exception ex)
            {
                SetError(false, ex);
            }
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SimpleAjaxAdd<TModel>(TModel model, Action<TModel> addAction, Func<SearchCriteria<TModel>, SearchResult<TModel>> searchAction, string modelListPartial)
        {
            JsonResultObject result = new JsonResultObject();

            try
            {
                if (!ModelState.IsValid)
                    ThrowModelStateErrors();

                addAction.Invoke(model);
                SetSuccess(result);

                SearchCriteria<TModel> searchCriteria = new SearchCriteria<TModel>();
                SearchResult<TModel> searchResult = searchAction.Invoke(searchCriteria);
                result.PartialViewHtml = RenderPartialViewToString(modelListPartial, searchResult);
            }
            catch (BusinessException ex)
            {
                SetError(result, false, ex);
            }

            return Json(result);
        }

        #endregion

    }



}