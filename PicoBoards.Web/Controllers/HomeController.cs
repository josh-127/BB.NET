using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Services;

namespace PicoBoards.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService userService;

        public HomeController(UserService userService)
            => this.userService = userService;

        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> Users()
        {
            var model = await userService.GetUserListings();
            return View(model);
        }
    }
}