
namespace PicoBoards.Security.Authentication.Models
{
    public class UserAccessToken
    {
        public int UserId { get; }

        public string EmailAddress { get; }

        public string UserName { get; }

        public UserAccessToken(int userId, string emailAddress, string userName)
            => (UserId, EmailAddress, UserName) = (userId, emailAddress, userName);
    }
}