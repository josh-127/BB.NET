using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;

namespace PicoBoards.Security.Authentication
{
    public class Registration : IModel, IValidatable
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Username")]
        [Identifier]
        [Required]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        public Registration() { }

        public Registration(string emailAddress, string userName, string password)
        {
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
        }
    }
}