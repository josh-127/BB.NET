using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PicoBoards.Security.Authentication;

namespace PicoBoards.Web.Models
{
    public sealed class LoginForm : Login
    {
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }

        public LoginForm() { }

        public LoginForm(string returnUrl)
            => ReturnUrl = returnUrl;
    }
}