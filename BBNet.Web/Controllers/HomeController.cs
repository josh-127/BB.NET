using System.Linq;
using BBNet.Data;
using BBNet.Web.ViewModels.Home;
using BBNet.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CommunityService communityService;

        public HomeController(CommunityService communityService)
            => this.communityService = communityService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Community()
        {
            var communities = communityService.GetAllCommunities();

            var communityListings = from c in communities
                                    select c.ToCommunityListing();

            var viewModel = new HomeCommunityViewModel
            {
                Communities = communityListings
            };

            return View(viewModel);
        }
    }
}