using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Web.Models
{
    public sealed class ChangeUserNameForm
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        public ChangeUserNameForm() { }

        public ChangeUserNameForm(string userName)
            => UserName = userName;
    }
}