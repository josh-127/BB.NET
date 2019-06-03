using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Security.Commands
{
    public sealed class SetUserEmailAddressCommand : IValidatable
    {
        public int UserId { get; }

        [EmailAddress]
        [Required]
        public string EmailAddress { get; }

        public SetUserEmailAddressCommand(int userId, string emailAddress)
            => (UserId, EmailAddress) = (userId, emailAddress);
    }
}