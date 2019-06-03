using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.ViewModels
{
    public sealed class DeleteAccountForm
    {
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}