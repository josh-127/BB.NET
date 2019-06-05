using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Security;
using PicoBoards.Security.Commands;
using PicoBoards.Security.Models;
using PicoBoards.Security.Queries;
using PicoBoards.Web.Features.Ucp.Forms;

namespace PicoBoards.Web.Features.Ucp
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
        private string EmailAddress => User.FindFirstValue(ClaimTypes.Email);
        private string UserName => User.FindFirstValue(ClaimTypes.Name);
        public UserAccessToken UserAccessToken => new UserAccessToken(UserId, EmailAddress, UserName);

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var model = await userService.QueryAsync(new UserProfileSettingsQuery(UserId));
            var viewModel = new EditUserProfileForm
            {
                Birthday = model.Birthday,
                Location = model.Location,
                Signature = model.Signature
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditUserProfileForm form)
        {
            if (ModelState.IsValid)
            {
                await userService.ExecuteAsync(new EditUserProfileCommand(
                    UserId, form.Birthday, form.Location, form.Signature));

                return RedirectToAction("EditProfile");
            }

            return View(form);
        }

        [HttpGet]
        public async Task<IActionResult> EditAccount()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var model = await userService.QueryAsync(new UserAccountSettingsQuery(UserId));
            var viewModel = new EditUserAccountForm
            {
                EmailAddress = model.EmailAddress,
                UserName = model.UserName,
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditAccount(EditUserAccountForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await userService.ExecuteAsync(new EditUserAccountCommand(
                        UserId,
                        form.EmailAddress,
                        form.UserName,
                        form.CurrentPassword,
                        form.NewPassword));

                    var currentUserId = UserId;
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, currentUserId.ToString(), ClaimValueTypes.Integer));
                    identity.AddClaim(new Claim(ClaimTypes.Name, form.UserName));
                    identity.AddClaim(new Claim(ClaimTypes.Email, form.EmailAddress));

                    var principal = new ClaimsPrincipal(identity);
                    var properties = new AuthenticationProperties();

                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        principal,
                        properties);

                    return RedirectToAction("EditAccount");
                }

                return View(form);
            }
            catch (CommandException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(form);
            }
        }

        [HttpGet]
        public IActionResult DeleteAccount()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            return View(new DeleteAccountForm());
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(DeleteAccountForm form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await userService.ExecuteAsync(new DeleteUserCommand(UserId, form.Password));
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                    return RedirectToAction("Index", "Forum");
                }

                return View(form);
            }
            catch (CommandException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(form);
            }
        }
    }
}