﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Security;
using PicoBoards.Security.Authentication;
using PicoBoards.Security.Queries;
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
        public UserAccessToken UserAccessToken => new UserAccessToken(UserId, UserName);

        [HttpGet]
        public IActionResult Index()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ChangeEmailAddress()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var emailAddress = await userService.GetUserEmailAddressAsync(new UserEmailAddressQuery(UserId));
            return View(new ChangeEmailAddressForm(emailAddress));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmailAddress(ChangeEmailAddressForm form)
        {
            try
            {
                await userService.SetEmailAddressAsync(UserId, form.EmailAddress);
                return RedirectToAction("Index");
            }
            catch (EditorException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(form);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ChangeUserName()
        {
            if (!IsAuthenticated)
                return RedirectToLogin();

            var userName = await userService.GetUserNameAsync(new UserNameQuery(UserId));
            return View(new ChangeUserNameForm(userName));
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserName(ChangeUserNameForm form)
        {
            try
            {
                await userService.SetUserNameAsync(UserId, form.UserName);
                return RedirectToAction("Index");
            }
            catch (EditorException e)
            {
                ModelState.AddModelError("", e.Message);
                return View(form);
            }
        }
    }
}