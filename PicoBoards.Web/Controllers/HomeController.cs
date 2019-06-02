using Microsoft.AspNetCore.Mvc;
using PicoBoards.Services;

namespace PicoBoards.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View();
    }
}