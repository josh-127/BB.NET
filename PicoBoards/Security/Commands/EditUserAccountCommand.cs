using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;

namespace PicoBoards.Security.Commands
{
    public sealed class EditUserAccountCommand : IValidatable
    {
        public int UserId { get; }

        [EmailAddress]
        [Required]
        public string EmailAddress { get; }

        [Identifier]
        [Required]
        public string UserName { get; }

        [Required]
        public string CurrentPassword { get; }

        public string NewPassword { get; }

        public EditUserAccountCommand(
            int userId,
            string emailAddress,
            string userName,
            string currentPassword,
            string newPassword)
        {
            UserId = userId;
            EmailAddress = emailAddress;
            UserName = userName;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }
    }
}