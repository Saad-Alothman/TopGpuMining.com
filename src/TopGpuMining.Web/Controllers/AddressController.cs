using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Exceptions;
using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Services;
using TopGpuMining.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TopGpuMining.Web.Controllers
{
    public class AddressController : BaseController
    {
        private readonly IAddressService _service;

        public AddressController(IAddressService service)
        {
            _service = service;
        }
        public IActionResult Index(AddressSearchViewModel model)
        {
            var result = _service.Search(model.ToSearchModel());

            if (IsAjaxRequest())
                return PartialView("_List", result);

            return View(result);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddressViewModel model)
        {
            try
            {
                var address = model.ToModel();

                _service.Add(address);

                SetSuccess();
            }
            catch (BusinessException ex)
            {
                SetError(ex);

                return View(model);
            }

            return RedirectToAction("Index");
        }
        
        public IActionResult Edit(string id)
        {
            var address = _service.GetById(id);

            var model = new AddressViewModel(address);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(AddressViewModel model)
        {
            try
            {
                var origin = _service.GetById(model.Id);

                origin = origin.Update(model.ToModel());

                _service.Save(origin);

                SetSuccess();
            }
            catch (BusinessException ex)
            {
                SetError(ex);

                return View("Edit", new { model.Id });
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            return SimpleAjaxAction(() =>
            {
                _service.Delete(id);
            });
        }
    }
}