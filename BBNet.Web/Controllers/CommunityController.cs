﻿using BBNet.Data;
using BBNet.Web.ViewModels.Communities;
using BBNet.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BBNet.Web.Controllers
{
    public class CommunityController : Controller
    {
        private readonly CommunityService communityService;
        private readonly ForumService forumService;

        public CommunityController(CommunityService communityService, ForumService forumService)
            => (this.communityService, this.forumService) = (communityService, forumService);

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

        [HttpGet]
        [Authorize]
        public IActionResult New()
        {
            return View(new NewCommunityViewModel());
        }

        [HttpPost]
        [Authorize]
        [AutoValidateAntiforgeryToken]
        public IActionResult New(NewCommunityViewModel submission)
        {
            if (!ModelState.IsValid)
                return View(submission);

            var community = new Community
            {
                Name = submission.Name,
                Description = submission.Description
            };

            communityService.AddCommunity(community);

            return RedirectToAction("Index", "Community", new { id = community.Id });
        }
    }
}