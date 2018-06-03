using System.Web.Mvc;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Core.Exceptions;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GmiAuthorizeStandardController<TModel, TService, TSearchCriteriaViewModelBase> : GmiStandardController<TModel, TService, TSearchCriteriaViewModelBase> where TModel : GmiEntityBase
        where TService : GmiServiceBase<TModel, TService>, new()
        where TSearchCriteriaViewModelBase : GmiSearchCriteriaViewModelBase<TModel>, new()
    {

    }

    public  class GmiStandardController<TModel, TService,TSearchCriteriaViewModelBase> : BaseController 
        where TModel: GmiEntityBase
        where TService : GmiServiceBase<TModel,TService>, new()
        where TSearchCriteriaViewModelBase : GmiSearchCriteriaViewModelBase<TModel>, new()
    {
        protected string ListPartialViewName = "~/Views/TModel/_List.cshtml";
        protected string FormPartialViewName = "~/Views/TModel/_Form.cshtml";
        protected string DeleteFormPartialViewName = "~/Views/TModel/_DeleteForm.cshtml";

        public GmiStandardController()
        {
            Init();
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
        
        private TService _service;
        private TService Service => _service ?? (_service = new TService());
        public virtual ActionResult Index()
        {
            return SimpleIndex<TModel>(Service.Search);
        }

        [HttpPost]
        public virtual ActionResult Add(TModel model)
        {
            return SimpleAjaxAdd(model, Service.Add, Service.Search, ListPartialViewName);
        }
        public virtual ActionResult Edit(int id)
        {
            JsonResultObject result = new JsonResultObject();

            var model = Service.GetById(id);

            result.PartialViewHtml = RenderPartialViewToString(FormPartialViewName, model);
            return Json(result);
        }


        [HttpPost]
        public virtual ActionResult Update(TModel model)
        {
            JsonResultObject result = new JsonResultObject();



            try
            {
                Service.Update(model);
                SetSuccess(result);

                var search = new SearchCriteria<TModel>();
                var modeltoView = Service.Search(search);
                result.PartialViewHtml = RenderPartialViewToString(ListPartialViewName, modeltoView);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }

            return Json(result);


        }



        public virtual ActionResult Delete(int id)
        {
            JsonResultObject result = new JsonResultObject();

            var model = Service.GetById(id);

            result.PartialViewHtml = RenderPartialViewToString(DeleteFormPartialViewName, model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual ActionResult Delete(TModel model)
        {
            JsonResultObject result = new JsonResultObject();
            try
            {
                Service.Delete(model.Id);
                SetSuccess(result);
                var search = new SearchCriteria<TModel>();
                var modeltoView = Service.Search(search);
                result.PartialViewHtml = RenderPartialViewToString(ListPartialViewName, modeltoView);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }

            return Json(result);
        }

        [HttpPost]
        public virtual ActionResult Search(TSearchCriteriaViewModelBase searchCriteriaViewModel)
        {
            return SimpleSearchAjaxAction<TModel, TSearchCriteriaViewModelBase>(searchCriteriaViewModel, ListPartialViewName, Service.Search);
        }
    }
}