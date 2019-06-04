using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Security.Authentication;
using PicoBoards.Web.Features.Auth.Forms;

namespace PicoBoards.Web.Features.Auth
{
    public class AuthController : Controller
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
            => this.authService = authService;

        [HttpGet]
        public IActionResult Login(string returnUrl)
            => View(new LoginForm { ReturnUrl = returnUrl ?? "/Home/Index" });

        [HttpPost]
        public async Task<IActionResult> Login(LoginForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await authService.ValidateUserAsync(form.ToLogin());
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.UserId.ToString(), ClaimValueTypes.Integer));
                    identity.AddClaim(new Claim(ClaimTypes.Name, result.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Email, result.EmailAddress));

                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties { IsPersistent = form.RememberMe };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        properties);

                    return LocalRedirect(form.ReturnUrl);
                }
            }
            catch (AuthenticationException e)
            {
                ModelState.AddModelError("", e.Message);
            }

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
            try
            {
                if (ModelState.IsValid)
                {
                    await authService.ExecuteAsync(form.ToRegistration());
                    return RedirectToAction("Login");
                }
            }
            catch (AuthenticationException e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(form);
        }
    }
}