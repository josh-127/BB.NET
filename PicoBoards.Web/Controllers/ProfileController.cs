using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Services;

namespace PicoBoards.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserService userService;

        public ProfileController(UserService userService)
            => this.userService = userService;

        public async Task<IActionResult> Index(string id)
        {
            var model = await userService.GetUserProfileAsync(id);
            return View(model);
        }
    }
}