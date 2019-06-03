using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;

namespace PicoBoards.Security.Authentication
{
    public sealed class Registration : IValidatable
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; private set; }

        [Identifier]
        [Required]
        public string UserName { get; private set; }

        [Required]
        public string Password { get; private set; }

        internal Registration() { }

        public Registration(string emailAddress, string userName, string password)
        {
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
        }
    }
}