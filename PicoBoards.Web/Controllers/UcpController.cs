using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Controllers
{
    public class UcpController : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Path });

            return View();
        }
    }
}