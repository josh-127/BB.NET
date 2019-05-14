using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    public class TopicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }
    }
}