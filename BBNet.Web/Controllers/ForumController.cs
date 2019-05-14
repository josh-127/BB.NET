using System;
using System.Collections.Generic;
using System.Linq;
using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService forumService;
        private readonly ITopicService topicService;

        public ForumController(IForumService forumService, ITopicService topicService)
            => (this.forumService, this.topicService) = (forumService, topicService);

        public IActionResult Index(int id)
        {
            var forum = forumService.GetForumById(id);
            var topics = topicService.GetTopicsByForumId(id);

            var forumListing = forum.ToForumListing();

            var topicListings = from t in topics
                                select t.ToTopicListing();

            var viewModel = new ForumIndexViewModel
            {
                Forum = forumListing,
                Topics = topicListings
            };

            return View(viewModel);
        }
    }
}