using System;

namespace PicoBoards.Security.Models
{
    public sealed class UserProfileSettings
    {
        public DateTime? Birthday { get; set; }

        public string Location { get; set; }

        public string Signature { get; set; }
    }
}