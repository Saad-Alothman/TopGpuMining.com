using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopGpuMining.Core;
using TopGpuMining.Core.Exceptions;
using TopGpuMining.Core.Interfaces;
using TopGpuMining.Core.Resources;
using TopGpuMining.Web.Helpers;
using TopGpuMining.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Search;
using TopGpuMining.Web.ViewModels;
using TopGpuMining.Web.Models;

namespace TopGpuMining.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IViewRender ViewRender => ServiceLocator.Current.GetService<IViewRender>();

        protected void SetSuccess(bool isAutoHide = true)
        {
            var alert = new Alert(MessageText.OperationSuccess, Alert.Type.Success, isAutoHide: isAutoHide);

            var alertJson = JsonConvert.SerializeObject(alert);

            TempData[WebConstants.ALERT] = alertJson;
        }

        protected void SetSuccess(JsonResultObject result, bool isAutoHide = true)
        {
            var alert = new Alert(MessageText.OperationSuccess, Alert.Type.Success, isAutoHide: isAutoHide);
            result.Alert = alert;
        }

        protected void SetError(string msg, bool isAutoHide = false)
        {
            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[WebConstants.ALERT] = alertJson;
        }

        protected void SetError(Exception ex = null, bool isAutoHide = false)
        {
            var msg = MessageText.OperationFailed;

            if (ex != null)
            {
                msg = GetExceptionError(ex);
            }

            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[WebConstants.ALERT] = alertJson;
        }

        protected void SetError(JsonResultObject result, Exception ex = null, bool isAutoHide = false)
        {
            var msg = MessageText.OperationFailed;

            if (ex != null)
            {
                msg = GetExceptionError(ex);
            }

            var alert = new Alert(msg, Alert.Type.Error, isAutoHide: isAutoHide);

            result.Alert = alert;
            result.Success = false;
        }

        protected void SetAlert(Alert alert)
        {
            var alertJson = JsonConvert.SerializeObject(alert);
            TempData[WebConstants.ALERT] = alertJson;
        }

        protected IActionResult SimpleAjaxAction(Action action, bool setSuccessMessage = true)
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
                SetError(result, ex);
                return BadRequest(result);
            }

            return Ok(result);
        }

        protected async Task<IActionResult> SimpleAjaxActionAsync(Func<Task> action, bool setSuccessMessage = true)
        {
            JsonResultObject result = new JsonResultObject();

            try
            {
                await action.Invoke();

                if (setSuccessMessage)
                    SetSuccess(result);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex);
                return BadRequest(result);
            }

            return Ok(result);
        }

        protected bool IsAjaxRequest()
        {
            if (Request == null)
                throw new ArgumentNullException(nameof(Request));

            if (Request.Headers != null)
                return Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            return false;
        }

        protected void ValidateModelState()
        {
            if (!ModelState.IsValid)
                throw new BusinessException(GetModalStateErrors());
        }

        protected List<string> GetModalStateErrors()
        {
            var result = new List<string>();

            foreach (var modelState in ViewData.ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    result.Add(error.ErrorMessage);
                }
            }

            return result;

        }
        
        private string GetExceptionError(Exception ex)
        {
            if (ex == null)
                return "";
            
            if (ex is BusinessException businessValidationMessage && businessValidationMessage.Errors?.Count > 0)
            {
                return FormatErrorMessage(businessValidationMessage.Errors);
            }

            return ex.Message;
        }

        protected string FormatErrorMessage(List<string> errors)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(MessageText.PleaseFixTheFollowingErrors);

            sb.Append("<ul>");

            foreach (var item in errors)
                sb.AppendLine($"<li>{item}</li>");

            sb.Append("</ul>");

            return sb.ToString();
        }


        //protected virtual IActionResult SimpleIndex<TModel>(Func<SearchCriteria<TModel>, SearchResult<TModel>> action)
        //  where TModel : BaseEntity
        //{
        //    SearchResult<TModel> viewModel = new SearchResult<TModel>();
        //    try
        //    {
                
        //        SearchCriteria<TModel> searchCriteria = new SearchCriteria<TModel>();
        //        viewModel = action.Invoke(searchCriteria);
        //    }
        //    catch (Exception ex)
        //    {
        //        SetError(ex);
        //    }
        //    return View(viewModel);
        //}


        [HttpPost]
        public IActionResult SimpleAjaxAdd<TModel>(TModel model, Func<TModel, TModel> addAction, Func<SearchCriteria<TModel>, SearchResult<TModel>> searchAction, string modelListPartial) where TModel :BaseEntity
        {
            return SimpleAjaxAction(() =>
            {
                ValidateModelState();

                addAction.Invoke(model);

            });
        }

        protected IActionResult SimpleSearchAjaxAction<T, TSearchCriteriaViewModel>(TSearchCriteriaViewModel model,
    string listPartialViewName, Func<SearchCriteria<T>, SearchResult<T>> action)
    where TSearchCriteriaViewModel : SearchViewModelBase<T>, new()
    where T : BaseEntity
        {
            if (model == null)
            {
                model = new TSearchCriteriaViewModel() { PageNumber = 1, PageSize = 10 };
            }
            ViewData[WebConstants.SEARCH_MODEL] = model;
            var searchCriteria = model.ToSearchModel();
            SearchResult<T> modelToView = action.Invoke(searchCriteria);
            
            if (IsAjaxRequest())
                return PartialView(listPartialViewName, modelToView);

            return View(modelToView);
        }
    }
}