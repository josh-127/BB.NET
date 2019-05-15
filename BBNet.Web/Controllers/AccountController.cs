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
        private readonly SignInManager<BBNetUser> signInManager;

        public AccountController(UserManager<BBNetUser> userManager, SignInManager<BBNetUser> signInManager)
            => (this.userManager, this.signInManager) = (userManager, signInManager);

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            await signInManager.SignOutAsync();

            return View(new AccountLoginViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(AccountLoginViewModel submission)
        {
            if (!ModelState.IsValid)
                return View(submission);

            var result = await signInManager.PasswordSignInAsync(
                submission.UserName, submission.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Incorrect username or password.");
                return View(submission);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

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