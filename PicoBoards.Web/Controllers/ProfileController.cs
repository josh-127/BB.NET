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

        public async Task<IActionResult> Index(string userName)
        {
            var model = await userService.GetUserProfile(userName);
            return View(model);
        }
    }
}