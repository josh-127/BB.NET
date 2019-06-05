using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;

namespace PicoBoards.Security.Commands
{
    public sealed class RegisterUserCommand : IValidatable
    {
        [EmailAddress]
        [Required]
        public string EmailAddress { get; }

        [Identifier]
        [Required]
        public string UserName { get; }

        [Required]
        public string Password { get; }

        internal RegisterUserCommand() { }

        public RegisterUserCommand(string emailAddress, string userName, string password)
        {
            EmailAddress = emailAddress;
            UserName = userName;
            Password = password;
        }
    }
}