using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BBNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ForumService forumService;

        public HomeController(ForumService forumService)
            => this.forumService = forumService;

        public IActionResult Index()
        {
            var forums = forumService.GetAllForums();

            var forumListings = from f in forums
                                select f.ToForumListing();

            var viewModel = new HomeIndexViewModel
            {
                Forums = forumListings
            };

            return View(viewModel);
        }
    }
}