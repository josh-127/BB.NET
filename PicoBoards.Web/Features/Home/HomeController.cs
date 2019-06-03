using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Features.Home
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}