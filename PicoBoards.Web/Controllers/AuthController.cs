using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Services;
using PicoBoards.Web.Models;

namespace PicoBoards.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService userService;

        public AuthController(UserService userService)
            => this.userService = userService;

        [HttpGet]
        public IActionResult Login(string returnUrl)
            => View(new LoginForm(returnUrl ?? "/Home/Index"));

        [HttpPost]
        public async Task<IActionResult> Login(LoginForm form)
        {
            var result = await userService.ValidateUserAsync(form);

            if (result.IsValid)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, form.UserName));

                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties
                {
                    IsPersistent = form.RememberMe
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    principal,
                    properties);

                return LocalRedirect(form.ReturnUrl);
            }

            ModelState.SetErrors(result);
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
            var result = await userService.RegisterUser(form);

            if (result.IsValid)
                return RedirectToAction("Login");

            ModelState.SetErrors(result);
            return View(form);
        }
    }
}