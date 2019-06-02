using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Services;

namespace PicoBoards.Web.Controllers
{
    public class GroupController : Controller
    {
        private readonly GroupService groupService;

        public GroupController(GroupService groupService)
            => this.groupService = groupService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await groupService.GetGroupListingsAsync();
            return View(model);
        }
    }
}