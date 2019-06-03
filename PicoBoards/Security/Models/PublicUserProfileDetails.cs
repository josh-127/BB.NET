using System;

namespace PicoBoards.Security.Models
{
    public class PublicUserProfileDetails : IModel, IValidatable
    {
        public int UserId { get; set; }

        public string EmailAddress { get; }

        public string UserName { get; }

        public string GroupName { get; }

        public DateTime Created { get; }

        public DateTime LastActive { get; }

        public DateTime? Birthday { get; }

        public string Location { get; }

        public string Signature { get; }

        public PublicUserProfileDetails(
            int userId,
            string emailAddress,
            string userName,
            string groupName,
            DateTime created,
            DateTime lastActive,
            DateTime? birthday,
            string location,
            string signature)
        {
            UserId = userId;
            EmailAddress = emailAddress;
            UserName = userName;
            GroupName = groupName;
            Created = created;
            LastActive = lastActive;
            Birthday = birthday;
            Location = location;
            Signature = signature;
        }
    }
}