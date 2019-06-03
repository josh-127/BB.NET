using System;

namespace PicoBoards.Security.Models
{
    public sealed class UserProfileSettings
    {
        public DateTime? Birthday { get; set; }

        public string Location { get; set; }

        public string Signature { get; set; }

        public UserProfileSettings(DateTime? birthday, string location, string signature)
            => (Birthday, Location, Signature) = (birthday, location, signature);
    }
}