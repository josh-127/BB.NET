using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PicoBoards.Web.Models
{
    public sealed class LoginModel
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; } = "/Home/Index";
    }
}