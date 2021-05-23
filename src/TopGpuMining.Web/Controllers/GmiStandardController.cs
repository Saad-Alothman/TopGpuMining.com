using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TopGpuMining.Application.Services;
using TopGpuMining.Core;
using TopGpuMining.Core.Entities;
using TopGpuMining.Core.Exceptions;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Services;
using TopGpuMining.Web.Models;
using TopGpuMining.Web.ViewModels;
using TopGpuMining.Web.ViewModels;

namespace TopGpuMining.Web.Controllers
{
    [Authorize(Roles = AppRoles.ADMIN_ROLE)]
    public class GmiAuthorizeStandardController<TModel, TService, TSearchViewModelBase> : GmiStandardController<TModel, TService, TSearchViewModelBase> where TModel : BaseEntity
        where TService : IGenericService<TModel>
        where TSearchViewModelBase : SearchViewModelBase<TModel>, new()
    {
        public GmiAuthorizeStandardController(TService service):base(service)
        {

        }
    }

    public class GmiStandardController<TModel, TService, TSearchViewModelBase> : BaseController
        where TModel : BaseEntity
        where TService : IGenericService<TModel>
        where TSearchViewModelBase : SearchViewModelBase<TModel>, new()
    {
        protected string ListPartialViewName = "~/Views/TModel/_List.cshtml";
        protected string FormPartialViewName = "~/Views/TModel/_Form.cshtml";
        protected string DeleteFormPartialViewName = "~/Views/TModel/_DeleteForm.cshtml";
        protected TService _service;
        public GmiStandardController(TService service)
        {
            Init();
            _service = service;
        }

        protected void Init()
        {
            SetPartialViewNames();
        }

        protected virtual void SetPartialViewNames()
        {

            string modelName = typeof(TModel).Name;
            string basePathToFolder = $"~/Views/{modelName}";
            this.ListPartialViewName = $"{basePathToFolder}/_List.cshtml";
            this.FormPartialViewName = $"{basePathToFolder}/_Form.cshtml";
            this.DeleteFormPartialViewName = $"{basePathToFolder}/_DeleteForm.cshtml";
        }

        
        public virtual IActionResult Index()
        {
            return SimpleIndex<TModel>(_service.Search);
        }
        protected override IActionResult SimpleIndex<TModel>(Func<SearchCriteria<TModel>, SearchResult<TModel>> action)
          where TModel : BaseEntity
        {
            SearchResult<TModel> viewModel = new SearchResult<TModel>();
            try
            {

                SearchCriteria<TModel> searchCriteria = new SearchCriteria<TModel>();
                viewModel = action.Invoke(searchCriteria);
            }
            catch (Exception ex)
            {
                SetError(ex);
            }
            return View(viewModel);
        }
        [HttpPost]
        public virtual IActionResult Add(TModel model)
        {
            return SimpleAjaxAdd(model,_service.Add, _service.Search, ListPartialViewName);
        }
        public virtual IActionResult Edit(string id)
        {
            JsonResultObject result = new JsonResultObject();

            var model = _service.GetById(id);

            result.PartialViewHtml =ViewRender.RenderAsync(FormPartialViewName, model).GetAwaiter().GetResult();
            return Json(result);
        }


        [HttpPost]
        public virtual IActionResult Update(TModel model)
        {
            JsonResultObject result = new JsonResultObject();



            try
            {
                _service.Save(model);
                SetSuccess(result);

                var search = new SearchCriteria<TModel>();
                var modeltoView = _service.Search(search);
                result.PartialViewHtml = ViewRender.RenderAsync(FormPartialViewName, model).GetAwaiter().GetResult();
            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }

            return Json(result);


        }



        public virtual IActionResult Delete(string id)
        {
            JsonResultObject result = new JsonResultObject();

            var model = _service.GetById(id);

            result.PartialViewHtml =ViewRender.RenderAsync(FormPartialViewName, model).GetAwaiter().GetResult();
            

            return Json(result);
        }

        [HttpPost]
        public virtual IActionResult Delete(TModel model)
        {
            JsonResultObject result = new JsonResultObject();
            try
            {
                _service.Delete(model.Id);
                SetSuccess(result);
                var search = new SearchCriteria<TModel>();
                var modeltoView = _service.Search(search);
                result.PartialViewHtml = ViewRender.RenderAsync(FormPartialViewName, model).GetAwaiter().GetResult();
            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }

            return Json(result);
        }

        [HttpPost]
        public virtual IActionResult Search(TSearchViewModelBase searchCriteriaViewModel)
        {
            return SimpleSearchAjaxAction<TModel, TSearchViewModelBase>(searchCriteriaViewModel, ListPartialViewName, _service.Search);
        }


    }
}