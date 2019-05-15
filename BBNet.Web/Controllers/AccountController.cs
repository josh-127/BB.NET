using System.Linq;
using System.Threading.Tasks;
using BBNet.Data;
using BBNet.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BBNet.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<BBNetUser> userManager;

        public AccountController(UserManager<BBNetUser> userManager)
            => this.userManager = userManager;

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new AccountRegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(AccountRegisterViewModel submission)
        {
            if (!ModelState.IsValid)
                return View(submission);

            var newUser = new BBNetUser
            {
                Email = submission.Email,
                UserName = submission.UserName
            };

            var result = await userManager.CreateAsync(newUser, submission.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(error.Code, error.Description);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}