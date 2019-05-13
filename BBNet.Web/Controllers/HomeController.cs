using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BBNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IForumService forumService;

        public HomeController(IForumService forumService)
            => this.forumService = forumService;

        public IActionResult Index()
        {
            var forums = forumService.GetAllForums();

            var forumListings = from f in forums
                                select new ForumListingViewModel
                                {
                                    Id = f.Id,
                                    Name = f.Name,
                                    Description = f.Description,
                                    ImageUrl = f.ImageUrl,
                                };

            var viewModel = new HomeIndexViewModel
            {
                Forums = forumListings
            };

            return View(viewModel);
        }
    }
}