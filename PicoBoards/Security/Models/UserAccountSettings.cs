
namespace PicoBoards.Security.Models
{
    public sealed class UserAccountSettings
    {
        public string EmailAddress { get; }

        public string UserName { get; }

        public UserAccountSettings(string emailAddress, string userName)
            => (EmailAddress, UserName) = (emailAddress, userName);
    }
}