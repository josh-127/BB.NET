using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Forums;
using PicoBoards.Forums.Commands;
using PicoBoards.Forums.Queries;
using PicoBoards.Web.ViewModels;

namespace PicoBoards.Web.Controllers
{
    public class AcpController : Controller
    {
        private readonly ForumService forumService;

        public AcpController(ForumService forumService)
            => this.forumService = forumService;

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Forums()
        {
            var model = await forumService.QueryAsync(new CategoryListingsQuery());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await forumService.ExecuteAsync(new AddCategoryCommand(form.Name));
                    return RedirectToAction("Index");
                }
            }
            catch (CommandException e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View("Forums");
        }
    }
}