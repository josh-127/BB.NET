using System.Linq;
using BBNet.Data;
using BBNet.Web.ViewModels;
using BBNet.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ForumService forumService;

        public AdminController(ForumService forumService)
            => this.forumService = forumService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Forums()
        {
            var forums = forumService.GetAllForums();

            var forumListings = from f in forums
                                select f.ToForumListing();

            var viewModel = new AdminForumsViewModel
            {
                Forums = forumListings
            };

            return View(viewModel);
        }
    }
}