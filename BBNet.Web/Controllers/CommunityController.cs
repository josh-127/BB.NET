using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BBNet.Web.Controllers
{
    public class CommunityController : Controller
    {
        private readonly ForumService forumService;

        public CommunityController(ForumService forumService)
            => this.forumService = forumService;

        [HttpGet]
        public IActionResult Index(int id)
        {
            var forums = forumService.GetForumsByCommunityId(id);

            var forumListings = from f in forums
                                select f.ToForumListing();

            var viewModel = new CommunityIndexViewModel
            {
                Forums = forumListings
            };

            return View(viewModel);
        }
    }
}