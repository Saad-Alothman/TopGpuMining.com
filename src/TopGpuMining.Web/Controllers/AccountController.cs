using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TopGpuMining.Application.Identities;
using TopGpuMining.Application.Services;
using TopGpuMining.Core.Exceptions;
using TopGpuMining.Core.Resources;
using TopGpuMining.Domain.Services;
using TopGpuMining.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TopGpuMining.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly TopGpuMiningUserManager _userManager;
        private readonly TopGpuMiningSignInManager _signInManager;
        private readonly IUserService _userService;
        private readonly ILogger _logger;
        

        public AccountController(
            TopGpuMiningUserManager userManager,
            TopGpuMiningSignInManager signInManager,
            IUserService userService,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            try
            {
                ValidateModelState();

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"User '{model.Email}' logged in.");

                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {

                }

                throw new BusinessException(MessageText.InvalidLoginAttempt);

            }
            catch (BusinessException ex)
            {
                SetError(ex);

                return View(model);
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            try
            {
                ValidateModelState();

                var user = model.ToModel();

                await _userService.AddAsync(user, model.Password);

            }
            catch (BusinessException ex)
            {
                SetError(ex);

                return View(model);
            }

            return RedirectToAction("index", "home");
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");

            return RedirectToAction("Login");
        }


        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }



    }

}