using System.Linq;
using System.Threading.Tasks;
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Services;
using TopGpuMining.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TopGpuMining.Web.Controllers
{

    public class CountryController : BaseController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        public async Task<IActionResult> Index(CountrySearchViewModel model)
        {
            var result = await _countryService.SearchAsync(model.ToSearchModel());
           
            if (!result.Result.Any() && result.PageNumber > 1)
            {
                model.PageNumber -= 1;

                result = await _countryService.SearchAsync(model.ToSearchModel());
            }

            if (IsAjaxRequest())
            {
                return PartialView("_List", result);
            }

            return View(result);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var entity = await _countryService.GetByIdAsync(id);

            var model = new CountryViewModel(entity);

            return PartialView("_Form", model);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Add(CountryViewModel model)
        {
            return SimpleAjaxActionAsync(async () =>
            {
                ValidateModelState();

                await _countryService.AddAsync(model.ToModel());

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Update(CountryViewModel model)
        {
            return SimpleAjaxActionAsync(async () =>
            {
                ValidateModelState();
                
                var origin = await _countryService.GetByIdAsync(model.Id);

                origin = origin.Update(model.ToModel());

                await _countryService.SaveAsync(origin);

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Delete(string id)
        {
            return SimpleAjaxActionAsync(async () =>
            {
                await _countryService.DeleteAsync(id);
            });
        }
    }
}