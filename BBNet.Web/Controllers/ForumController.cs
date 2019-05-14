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
        private readonly ForumService forumService;
        private readonly TopicService topicService;

        public ForumController(ForumService forumService, TopicService topicService)
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

        [HttpGet]
        public IActionResult New()
        {
            return View(new ForumNewViewModel());
        }

        [HttpPost]
        [ActionName("New")]
        public IActionResult NewSubmission(ForumNewViewModel submission)
        {
            var forum = BuildForum(submission);
            forumService.AddForum(forum);

            return RedirectToAction("Index", "Forum", new { id = forum.Id });
        }

        private Forum BuildForum(ForumNewViewModel submission)
            => new Forum
        {
            Name = submission.Name,
            Description = submission.Description,
            ImageUrl = submission.ImageUrl
        };
    }
}