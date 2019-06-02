using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Models;

namespace PicoBoards.Web.Controllers
{
    public class UcpController : Controller
    {
        private IActionResult RedirectToLogin()
            => RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Path });

        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToLogin();

            var model = new Dashboard(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View(model);
        }

        [HttpGet]
        public IActionResult AccountSettings()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToLogin();

            return View();
        }

        [HttpGet]
        public IActionResult ProfileSettings()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToLogin();

            return View();
        }
    }
}