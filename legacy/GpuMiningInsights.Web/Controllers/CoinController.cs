using System;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Web.Mvc;
using CreaDev.Framework.Core.Linq;
using CreaDev.Framework.Core.Models;
using CreaDev.Framework.Web.Mvc.Models;
using GpuMiningInsights.Application.Services;
using GpuMiningInsights.Domain.Models;
using GpuMiningInsights.Web.Helpers;
using GpuMiningInsights.Web.Models.Search;

namespace GpuMiningInsights.Web.Controllers
{
    public class CoinController : GmiStandardController<Coin, CoinService, CoinSearchCrietriaViewModel>
    {
        [HttpPost]
        public override ActionResult Search(CoinSearchCrietriaViewModel model)
        {

            if (model == null)
            {
                model = new CoinSearchCrietriaViewModel() { PageNumber = 1, PageSize = 100 };
            }
            
            ViewData[Constants.SEARCH_MODEL] = model;
            var searchCriteria = model.ToSearchCriteria();
            searchCriteria.FilterExpression = searchCriteria.FilterExpression.And(coin => coin.ExchangeRateUsd != null);
            searchCriteria.SortExpression = (coin =>coin.OrderByDescending(c=> c.ExchangeRateUsd ) );
            SearchResult<Coin> modelToView = CoinService.Instance.Search(searchCriteria);

            if (Request.IsAjaxRequest())
                return PartialView(ListPartialViewName, modelToView);

            return View(modelToView);
        }
        public override ActionResult Index()
        {
            SearchResult<Coin> viewModel = new SearchResult<Coin>();
            try
            {
                CoinSearchCrietriaViewModel model = new CoinSearchCrietriaViewModel() { PageNumber = 1, PageSize = 100 };
                SearchCriteria<Coin> searchCriteria = model.ToSearchCriteria();
                searchCriteria.FilterExpression = searchCriteria.FilterExpression.And(coin => coin.ExchangeRateUsd != null);
                searchCriteria.SortExpression = (coin => coin.OrderByDescending(c => c.ExchangeRateUsd));
                viewModel = CoinService.Instance.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                SetError(false, ex);
            }
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override ActionResult Add(Coin model)
        {
            return base.Add(model);
        }
        [Authorize(Roles = "Admin")]
        public override ActionResult Edit(int id)
        {

            return base.Edit(id);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override ActionResult Update(Coin model)
        {
            return base.Update(model);
        }



        [Authorize(Roles = "Admin")]
        public override ActionResult Delete(int id)
        {
            return base.Delete(id);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override ActionResult Delete(Coin model)
        {
            return base.Delete(model);
        }
    }
}