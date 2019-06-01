using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.Models
{
    public sealed class RegisterModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Username")]
        [RegularExpression(
            "[A-Za-z0-9_]+",
            ErrorMessage = "Username can only contain alphanumeric characters and '_'.")]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}