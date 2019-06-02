using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Services;

namespace PicoBoards.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserService userService)
            => this.userService = userService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await userService.GetUserListingsAsync();
            return View(model);
        }
    }
}