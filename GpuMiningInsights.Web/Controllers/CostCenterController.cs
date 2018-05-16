using System.Web.Mvc;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Core.Exceptions;
using GpuMiningInsights.Domain.Models;

namespace GpuMiningInsights.Web.Controllers
{
    public class BrandController : BaseController
    {
        private string BrandListPartial = "~/Views/Brand/_BrandList.cshtml";
        private string BrandFormPartial = "~/Views/Brand/_BrandForm.cshtml";
        private string BrandDeleteFormPartial = "~/Views/Brand/_DeleteForm.cshtml";
        public ActionResult Index(int? page)
        {
            var search = new SearchCriteria<Brand>(page);

            var model = BrandService.Instance.Search(search);

            if (Request.IsAjaxRequest())
                return PartialView(BrandListPartial, model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Brand model)
        {
            JsonResultObject result = new JsonResultObject();

            try
            {
                if (!ModelState.IsValid)
                    ThrowModelStateErrors();

                BrandService.Instance.Add(model);
                SetSuccess(result);

                var search = new SearchCriteria<Brand>();
                var modeltoView = BrandService.Instance.Search(search);
                result.PartialViewHtml = RenderPartialViewToString(BrandListPartial, modeltoView);
            }
            catch (BusinessException ex)
            {
                SetError(result, false, ex);
            }

            return Json(result);
        }


        public ActionResult Edit(int id)
        {
            JsonResultObject result = new JsonResultObject();

            var model = BrandService.Instance.GetById(id);

            result.PartialViewHtml = RenderPartialViewToString(BrandFormPartial, model);
            return Json(result);
        }

        [HttpPost]
        public ActionResult Update(Brand model)
        {
            JsonResultObject result = new JsonResultObject();



            try
            {
                BrandService.Instance.Update(model);
                SetSuccess(result);

                var search = new SearchCriteria<Brand>();
                var modeltoView = BrandService.Instance.Search(search);
                result.PartialViewHtml = RenderPartialViewToString(BrandListPartial, modeltoView);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }

            return Json(result);


        }



        public ActionResult Delete(int id)
        {
            JsonResultObject result = new JsonResultObject();

            var model = BrandService.Instance.GetById(id);

            result.PartialViewHtml = RenderPartialViewToString(BrandDeleteFormPartial, model);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(Brand model)
        {
            JsonResultObject result = new JsonResultObject();

            try
            {
                BrandService.Instance.Delete(model.ID);
                SetSuccess(result);
                var search = new SearchCriteria<Brand>();
                var modeltoView = BrandService.Instance.Search(search);
                result.PartialViewHtml = RenderPartialViewToString(BrandListPartial, modeltoView);
            }
            catch (BusinessException ex)
            {
                SetError(result, ex: ex);
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult Search(BrandSearchViewModel model)
        {
            return SimpleSearchAjaxActionSearchViewModelBase<Brand, BrandSearchViewModel>(model, BrandListPartial, criteria => BrandService.Instance.Search(model.ToSearchModel()));
        }

    }
}