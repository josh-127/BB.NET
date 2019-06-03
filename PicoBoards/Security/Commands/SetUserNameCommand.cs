using System.ComponentModel.DataAnnotations;
using PicoBoards.DataAnnotations;

namespace PicoBoards.Security.Commands
{
    public sealed class SetUserNameCommand : IValidatable
    {
        public int UserId { get; }

        [Identifier]
        [Required]
        public string UserName { get; }

        public SetUserNameCommand(int userId, string userName)
            => (UserId, UserName) = (userId, userName);
    }
}