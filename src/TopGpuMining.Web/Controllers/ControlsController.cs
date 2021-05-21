using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopGpuMining.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using TopGpuMining.Core.Extensions;
using System.Text;
using TopGpuMining.Web.Models;
using TopGpuMining.Application.Services;
using TopGpuMining.Domain.Models;
using TopGpuMining.Domain.Services;

namespace TopGpuMining.Web.Controllers
{
    public class ControlsController : BaseController
    {
        public const string PARTIAL_ADDRESS_FORM = "~/Views/Address/_Form.cshtml";

        private readonly IAddressService _addressService;

        public ControlsController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public IActionResult Index()
        {
            var model = new ControlsViewModel
            {
                Address = _addressService.GetById("0000-0000000-000000-00001")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(ControlsViewModel model)
        {
            StringBuilder msg = new StringBuilder();

            SetSuccess();

            msg.Append("<ul>");

            msg.Append($"<li>Start Date: {model.StartDate?.ToSystemDate()}</li>");

            msg.Append($"<li>End Date: {model.EndDate?.ToSystemDate()}</li>");

            msg.Append($"<li>Enabeld: {model.Enabled.ToString()}</li>");

            msg.Append("</ul>");

            SetAlert(new Models.Alert()
            {
                IsAutoHide = false,
                AlertType = Models.Alert.Type.Info,
                Message = msg.ToString()
            });
            

            return RedirectToAction(nameof(Index));
        }
        
        public IActionResult InlineForm()
        {
            var model = new ControlsViewModel
            {
                Address = _addressService.GetById("0000-0000000-000000-00001")
            };

            return View(model);
        }

        public IActionResult Inputs()
        {
            return View();
        }
        
        public IActionResult Tabs()
        {
            var model = new ControlsViewModel
            {
                Address = _addressService.GetById("0000-0000000-000000-00001")
            };

            return View(model);
        }

        public IActionResult Wizard()
        {
            return View();
        }

        public IActionResult EditAddress(string id)
        {
            var address = _addressService.GetById(id);

            var model = new AddressViewModel(address);

            return PartialView(PARTIAL_ADDRESS_FORM, model);
        }

        public IActionResult Dates()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dates(DateViewModel model)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("You have submitted");
            sb.Append("<ul>");
            sb.Append($"<li>{model.Date}</li>");

            SetAlert(new Alert(sb.ToString(), Alert.Type.Info,false));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAddress(Address model)
        {
            return SimpleAjaxAction(() =>
            {
                ValidateModelState();

                var address = _addressService.GetById(model.Id);

                address = address.Update(model);

                _addressService.Save(address);
            });
        }
    }
}