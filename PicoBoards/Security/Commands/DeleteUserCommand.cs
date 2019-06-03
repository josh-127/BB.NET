using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Security.Commands
{
    public sealed class DeleteUserCommand : IValidatable
    {
        public int UserId { get; }

        [Required]
        public string Password { get; }

        public DeleteUserCommand(int userId, string password)
            => (UserId, Password) = (userId, password);
    }
}