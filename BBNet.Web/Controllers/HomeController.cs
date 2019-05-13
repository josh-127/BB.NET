using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}