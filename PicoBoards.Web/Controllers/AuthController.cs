﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Security.Authentication;
using PicoBoards.Web.Models;

namespace PicoBoards.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
            => this.authService = authService;

        [HttpGet]
        public IActionResult Login(string returnUrl)
            => View(new LoginForm(returnUrl ?? "/Home/Index"));

        [HttpPost]
        public async Task<IActionResult> Login(LoginForm form)
        {
            var result = await authService.ValidateUserAsync(form);

            if (result.IsSuccessful)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(
                    ClaimTypes.NameIdentifier, result.Value.UserId.ToString(), ClaimValueTypes.Integer));
                identity.AddClaim(new Claim(ClaimTypes.Name, result.Value.UserName));

                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties { IsPersistent = form.RememberMe };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    properties);

                return LocalRedirect(form.ReturnUrl);
            }

            ModelState.SetErrors(result.Error);
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationForm form)
        {
            var result = await authService.RegisterUserAsync(form);

            if (result.IsValid)
                return RedirectToAction("Login");

            ModelState.SetErrors(result);
            return View(form);
        }
    }
}