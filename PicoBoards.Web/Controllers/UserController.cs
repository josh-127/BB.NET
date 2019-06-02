using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Services;

namespace PicoBoards.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly GroupService groupService;

        public UserController(UserService userService, GroupService groupService)
            => (this.userService, this.groupService) = (userService, groupService);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await userService.GetUserListingsAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Profile(int id)
        {
            var model = await userService.GetUserProfileAsync(id);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Group()
        {
            var model = await groupService.GetGroupListingsAsync();
            return View(model);
        }
    }
}