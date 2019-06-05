using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Forums;
using PicoBoards.Forums.Commands;
using PicoBoards.Forums.Queries;
using PicoBoards.Web.Features.Forum.Forms;

namespace PicoBoards.Web.Features.Forum
{
    public class ForumController : Controller
    {
        private readonly ForumService forumService;

        public ForumController(ForumService forumService)
            => this.forumService = forumService;

        private IActionResult RedirectToLogin()
            => RedirectToAction("Login", "Auth", new { returnUrl = Request.Path });

        private bool IsAuthenticated => User.Identity.IsAuthenticated;
        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string EmailAddress => User.FindFirstValue(ClaimTypes.Email);
        private string UserName => User.FindFirstValue(ClaimTypes.Name);

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await forumService.QueryAsync(new AllCategoryDetailsQuery());
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Forum(int id)
        {
            var model = await forumService.QueryAsync(new ForumDetailsQuery(id));
            return View(model);
        }

        [HttpGet]
        public IActionResult NewTopic(int id)
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            return View(new NewTopicForm(id));
        }

        [HttpPost]
        public async Task<IActionResult> NewTopic(NewTopicForm form)
        {
            try
            {
                var topicId = await forumService.ExecuteAsync(
                    new AddTopicCommand(
                        form.ForumId,
                        UserId,
                        form.Name,
                        form.Description,
                        form.OpeningPostBody,
                        form.FormattingEnabled,
                        form.SmiliesEnabled,
                        form.ParseUrls,
                        form.AttachSignature));

                return RedirectToAction("Topic", new { id = topicId });
            }
            catch (CommandException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(form);
            }
        }
    }
}