using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}