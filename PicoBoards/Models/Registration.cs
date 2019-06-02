using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Models
{
    public class Registration : IModel, IValidatable
    {
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        [Required]
        public string EmailAddress { get; set; }

        [Display(Name = "Username")]
        [RegularExpression(
            "[A-Za-z0-9_]+",
            ErrorMessage = "Username can only contain alphanumeric characters and '_'.")]
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