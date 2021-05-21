using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Services;
using TopGpuMining.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TopGpuMining.Web.Controllers.Api
{
    public class CountryController : ApiBaseController
    {
        private readonly ICountryService _countryService;

        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [Route("search")]
        public async Task<IActionResult> Search(CountrySearchViewModel model)
        {
            try
            {
                var result = new List<object>();

                var data = await _countryService.SearchAsync(model.ToSearchModel());
                
                var countries = data.Result;

                foreach (var item in countries)
                {
                    result.Add(new
                    {
                        Id = item.Id,
                        Name = item.NameArabic,
                        NameEn = item.NameEnglish,
                        CodeTwo = item.CodeTwo
                    });
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}