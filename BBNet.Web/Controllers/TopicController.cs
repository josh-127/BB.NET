using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BBNet.Web.Controllers
{
    public class TopicController : Controller
    {
        private readonly IForumService forumService;
        private readonly ITopicService topicService;

        public TopicController(IForumService forumService, ITopicService topicService)
            => (this.forumService, this.topicService) = (forumService, topicService);

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult New(int id)
        {
            return View(new TopicNewViewModel { ForumId = id });
        }

        [HttpPost]
        [ActionName("New")]
        public IActionResult New(TopicNewViewModel submission)
        {
            var now = DateTime.Now;

            var forum = forumService.GetForumById(submission.ForumId);
            var topic = BuildTopic(submission, now);
            var openingPost = BuildOpeningPost(submission, now);
            topicService.AddTopic(topic, openingPost, forum);

            return RedirectToAction("Index", "Topic", new { id = topic.Id });
        }

        private Topic BuildTopic(TopicNewViewModel submission, DateTime created)
            => new Topic
            {
                Title = submission.Title,
                Description = submission.Description,
                Created = created
            };

        private Post BuildOpeningPost(TopicNewViewModel submission, DateTime created)
            => new Post
            {
                Title = submission.Title,
                Body = submission.OpeningPostBody
            };
    }
}