using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBNet.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IForumService forumService;

        public AdminController(IForumService forumService)
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