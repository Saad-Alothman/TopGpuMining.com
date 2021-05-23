using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Extensions;
using TopGpuMining.Core.Search;
using TopGpuMining.Domain.Models;
using TopGpuMining.Web.Helpers;
using TopGpuMining.Web.ViewModels.Search;

namespace TopGpuMining.Web.Controllers
{
    public class CoinController : GmiStandardController<Coin, CoinService, CoinSearchCrietriaViewModel>
    {
        public CoinController(CoinService service) : base(service)
        {
        }

        [HttpPost]
        public override IActionResult Search(CoinSearchCrietriaViewModel model)
        {

            if (model == null)
            {
                model = new CoinSearchCrietriaViewModel() { PageNumber = 1, PageSize = 100 };
            }
            
            ViewData[WebConstants.SEARCH_MODEL] = model;
            var searchCriteria = model.ToSearchModel();
            searchCriteria.FilterExpression = searchCriteria.FilterExpression.And(coin => coin.ExchangeRateUsd != null);
            searchCriteria.SortExpression = (coin =>coin.OrderByDescending(c=> c.ExchangeRateUsd ) );
            SearchResult<Coin> modelToView = _service.Search(searchCriteria);

            if (IsAjaxRequest())
                return PartialView(ListPartialViewName, modelToView);

            return View(modelToView);
        }
        public override IActionResult Index()
        {
            SearchResult<Coin> viewModel = new SearchResult<Coin>();
            try
            {
                CoinSearchCrietriaViewModel model = new CoinSearchCrietriaViewModel() { PageNumber = 1, PageSize = 100 };
                SearchCriteria<Coin> searchCriteria = model.ToSearchModel();
                searchCriteria.FilterExpression = searchCriteria.FilterExpression.And(coin => coin.ExchangeRateUsd != null);
                searchCriteria.SortExpression = (coin => coin.OrderByDescending(c => c.ExchangeRateUsd));
                viewModel = _service.Search(searchCriteria);
            }
            catch (Exception ex)
            {
                SetError(ex);
            }
            return View(viewModel);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override IActionResult Add(Coin model)
        {
            return base.Add(model);
        }
        [Authorize(Roles = "Admin")]
        public override IActionResult Edit(string id)
        {

            return base.Edit(id);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override IActionResult Update(Coin model)
        {
            return base.Update(model);
        }



        [Authorize(Roles = "Admin")]
        public override IActionResult Delete(string id)
        {
            return base.Delete(id);

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override IActionResult Delete(Coin model)
        {
            return base.Delete(model);
        }
    }
}