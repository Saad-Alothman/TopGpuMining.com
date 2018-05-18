using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Core.Exceptions;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class BrandController : GmiStandardController<Brand, BrandService, BrandSearchCrietriaViewModel>
    {

    }
    //public class BrandController : BaseController
    //{
    //    public BrandController()
    //    {

    //    }
    //    private string BrandListPartial = "~/Views/Brand/_List.cshtml";
    //    private string BrandFormPartial = "~/Views/Brand/_Form.cshtml";
    //    private string BrandDeleteFormPartial = "~/Views/Brand/_DeleteForm.cshtml";
    //    public ActionResult Index()
    //    {
    //        return SimpleIndex<Brand>(BrandService.Instance.Search);
    //    }

    //    [HttpPost]
    //    public ActionResult Add(Brand model)
    //    {
    //        return SimpleAjaxAdd(model, BrandService.Instance.Add, BrandService.Instance.Search, BrandListPartial);
    //    }


    //    public ActionResult Edit(int id)
    //    {
    //        JsonResultObject result = new JsonResultObject();

    //        var model = BrandService.Instance.GetById(id);

    //        result.PartialViewHtml = RenderPartialViewToString(BrandFormPartial, model);
    //        return Json(result);
    //    }

    //    [HttpPost]
    //    public ActionResult Update(Brand model)
    //    {
    //        JsonResultObject result = new JsonResultObject();



    //        try
    //        {
    //            BrandService.Instance.Update(model);
    //            SetSuccess(result);

    //            var search = new SearchCriteria<Brand>();
    //            var modeltoView = BrandService.Instance.Search(search);
    //            result.PartialViewHtml = RenderPartialViewToString(BrandListPartial, modeltoView);
    //        }
    //        catch (BusinessException ex)
    //        {
    //            SetError(result, ex: ex);
    //        }

    //        return Json(result);


    //    }



    //    public ActionResult Delete(int id)
    //    {
    //        JsonResultObject result = new JsonResultObject();

    //        var model = BrandService.Instance.GetById(id);

    //        result.PartialViewHtml = RenderPartialViewToString(BrandDeleteFormPartial, model);

    //        return Json(result, JsonRequestBehavior.AllowGet);
    //    }

    //    [HttpPost]
    //    public ActionResult Delete(Brand model)
    //    {
    //        JsonResultObject result = new JsonResultObject();

    //        try
    //        {
    //            BrandService.Instance.Delete(model.Id);
    //            SetSuccess(result);
    //            var search = new SearchCriteria<Brand>();
    //            var modeltoView = BrandService.Instance.Search(search);
    //            result.PartialViewHtml = RenderPartialViewToString(BrandListPartial, modeltoView);
    //        }
    //        catch (BusinessException ex)
    //        {
    //            SetError(result, ex: ex);
    //        }

    //        return Json(result);
    //    }

    //    //[HttpPost]
    //    //public ActionResult Search(BrandSearchViewModel model)
    //    //{
    //    //    return SimpleSearchAjaxActionSearchViewModelBase<Brand, BrandSearchViewModel>(model, BrandListPartial, criteria => BrandService.Instance.Search(model.ToSearchModel()));
    //    //}

    //}
}