using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Security.Authentication
{
    public class Login : IModel, IValidatable
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public Login() { }

        public Login(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}