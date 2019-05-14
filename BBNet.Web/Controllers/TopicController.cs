using System;
using System.Linq;
using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    public class TopicController : Controller
    {
        private readonly IForumService forumService;
        private readonly ITopicService topicService;
        private readonly IPostService postService;

        public TopicController(IForumService forumService, ITopicService topicService, IPostService postService)
        {
            this.forumService = forumService;
            this.topicService = topicService;
            this.postService = postService;
        }

        public IActionResult Index(int id)
        {
            var topic = topicService.GetTopicById(id);
            var posts = postService.GetPostsByTopicId(id);

            var postListings = from p in posts
                               select new PostListingViewModel
                               {
                                   Id = p.Id,
                                   Title = p.Title,
                                   Body = p.Body,
                                   Created = p.Created
                               };

            var viewModel = new TopicIndexViewModel
            {
                Topic = topic.ToTopicListing(),
                Posts = postListings
            };

            return View(viewModel);
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

        [HttpGet]
        public IActionResult NewPost(int id)
        {
            var topic = topicService.GetTopicById(id);

            return View(new TopicNewPostViewModel
            {
                TopicId = id,
                Title = topic.Title,
                Body = ""
            });
        }

        [HttpPost]
        public IActionResult NewPost(TopicNewPostViewModel submission)
        {
            var now = DateTime.Now;

            var topicId = submission.TopicId;
            var topic = topicService.GetTopicById(topicId);
            var post = BuildPost(submission, now);

            postService.AddPost(post, topic);

            return RedirectToAction("Index", "Topic", new { id = topicId });
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
                Body = submission.OpeningPostBody,
                Created = created
            };

        private Post BuildPost(TopicNewPostViewModel submission, DateTime created)
            => new Post
            {
                Title = submission.Title,
                Body = submission.Body,
                Created = created
            };
    }
}