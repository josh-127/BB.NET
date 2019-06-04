using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Forums;
using PicoBoards.Forums.Queries;

namespace PicoBoards.Web.Features.Home
{
    public class HomeController : Controller
    {
        private readonly ForumService forumService;

        public HomeController(ForumService forumService)
            => this.forumService = forumService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await forumService.QueryAsync(new AllCategoryDetailsQuery());
            return View(model);
        }
    }
}