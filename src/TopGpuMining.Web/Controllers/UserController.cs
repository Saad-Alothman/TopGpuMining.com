using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopGpuMining.Application.Identities;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Services;
using TopGpuMining.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TopGpuMining.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly TopGpuMiningUserManager _userManager;
        private readonly TopGpuMiningSignInManager _signInManager;
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public const string PARTIAL_LIST = "~/Views/User/_List.cshtml";
        public const string PARTIAL_FORM = "~/Views/User/Forms/_EditForm.cshtml";

        public UserController(TopGpuMiningUserManager userManager,
            TopGpuMiningSignInManager signInManager,
            IUserService userService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
        }
        
        
        public IActionResult Index(UserSearchViewModel model)
        {
            var result = _userService.Search(model.ToSearchModel());

            if (IsAjaxRequest())
                return PartialView(PARTIAL_LIST, result);

            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddUserViewModel model)
        {
            return SimpleAjaxAction(() =>
            {
                ValidateModelState();

                _userService.AddAsync(model.ToModel(), model.Password);

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            return SimpleAjaxAction(() =>
            {
                _userService.Delete(id);
            });
        }

        public IActionResult Edit(string id)
        {
            var user = _userService.GetById(id);

            var model = new UserViewModel(user);

            return PartialView(PARTIAL_FORM, model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UserViewModel model)
        {
            return SimpleAjaxAction(() =>
            {
                _userService.Save(model.ToModel(),model.Password);
            });
        }
    }
}