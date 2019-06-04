using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Forums;
using PicoBoards.Forums.Commands;
using PicoBoards.Forums.Queries;
using PicoBoards.Web.Features.Acp.Forms;

namespace PicoBoards.Web.Features.Acp
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
                    return RedirectToAction("Forums");
                }
            }
            catch (CommandException e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View("Forums");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            try
            {
                await forumService.ExecuteAsync(new RemoveCategoryCommand(id));
                return RedirectToAction("Forums");
            }
            catch (CommandException e)
            {
                ModelState.AddModelError("", e.Message);
                return View("Forums");
            }
        }

        [HttpGet]
        public IActionResult EditCategory(int id)
            => View(new EditCategoryForm(id));

        [HttpPost]
        public async Task<IActionResult> EditCategory(EditCategoryForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await forumService.ExecuteAsync(new EditCategoryCommand(
                        form.CategoryId, form.Name));

                    return RedirectToAction("Forums");
                }
            }
            catch (CommandException e)
            {
                ModelState.AddModelError("", e.Message);
            }

            return View(new EditCategoryForm(form.CategoryId));
        }
    }
}