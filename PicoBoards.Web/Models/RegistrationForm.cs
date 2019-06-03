using System.ComponentModel.DataAnnotations;
using PicoBoards.Security.Authentication;

namespace PicoBoards.Web.Models
{
    public sealed class RegistrationForm : Registration
    {
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}