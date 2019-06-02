using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Models;
using PicoBoards.Services;
using PicoBoards.Web.Models;

namespace PicoBoards.Web.Controllers
{
    public class UcpController : Controller
    {
        private readonly UserService userService;

        public UcpController(UserService userService)
            => this.userService = userService;

        private IActionResult RedirectToLogin()
            => RedirectToAction("Login", "Auth", new { returnUrl = HttpContext.Request.Path });

        private bool IsAuthenticated => User.Identity.IsAuthenticated;

        private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private string UserName => User.FindFirstValue(ClaimTypes.Name);

        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToLogin();

            var model = new Dashboard(UserId, UserName);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEmailAddress()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var emailAddress = await userService.GetUserEmailAddressAsync(UserId);
            return View(new ChangeEmailAddressForm(emailAddress));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmailAddress(ChangeEmailAddressForm form)
        {
            await userService.SetUserEmailAddressAsync(UserId, form.EmailAddress);
            return RedirectToAction("Index");
        }
    }
}