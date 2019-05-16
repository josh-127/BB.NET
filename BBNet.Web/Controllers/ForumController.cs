using System.Linq;
using BBNet.Data;
using BBNet.Web.ViewModels.Forums;
using BBNet.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    public class ForumController : Controller
    {
        private readonly CommunityService communityService;
        private readonly ForumService forumService;
        private readonly TopicService topicService;

        public ForumController(
            CommunityService communityService, ForumService forumService, TopicService topicService)
        {
            this.communityService = communityService;
            this.forumService = forumService;
            this.topicService = topicService;
        }

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
        public IActionResult New(int id)
        {
            return View(new ForumNewViewModel { CommunityId = id });
        }

        [HttpPost]
        [ActionName("New")]
        public IActionResult NewSubmission(ForumNewViewModel submission)
        {
            var community = communityService.GetCommunityById(submission.CommunityId);
            var forum = BuildForum(submission);
            forumService.AddForum(forum, community);

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