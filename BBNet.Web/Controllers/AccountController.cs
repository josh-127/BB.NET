using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using BBNet.Web.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace BBNet.Web.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager authManager
            => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        public ActionResult LogIn(string returnUrl)
        {
            return View(new LogInModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            var redirectUrl = model.ReturnUrl ?? "/Home/Index";

            if (!ModelState.IsValid)
                return View(redirectUrl);

            if (model.UserName == "admin" && model.Password == "password")
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, model.UserName));

                var properties = new AuthenticationProperties();

                authManager.SignIn(properties, identity);

                return Redirect(redirectUrl);
            }

            return View(redirectUrl);
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            authManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);

            return RedirectToAction("Index", "Home");
        }
    }
}