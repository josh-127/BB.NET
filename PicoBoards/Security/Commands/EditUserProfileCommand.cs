using System;

namespace PicoBoards.Security.Commands
{
    public sealed class EditUserProfileCommand : IValidatable
    {
        public int UserId { get; }

        public DateTime? Birthday { get; }

        public string Location { get; }

        public string Signature { get; }

        public EditUserProfileCommand(int userId, DateTime? birthday, string location, string signature)
            => (UserId, Birthday, Location, Signature) = (userId, birthday, location, signature);
    }
}