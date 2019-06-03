using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;

namespace PicoBoards.Security.Authentication.Commands
{
    public sealed class RegisterUserCommand : IValidatable
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; private set; }

        [Identifier]
        [Required]
        public string UserName { get; private set; }

        [Required]
        public string Password { get; private set; }

        internal RegisterUserCommand() { }

        public RegisterUserCommand(string emailAddress, string userName, string password)
        {
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
        }
    }
}