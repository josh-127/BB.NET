
namespace PicoBoards.Security.Authentication.Models
{
    public class UserAccessToken
    {
        public int UserId { get; set; }

        public string EmailAddress { get; set; }

        public string UserName { get; set; }

        public UserAccessToken() { }

        public UserAccessToken(int userId, string emailAddress, string userName)
            => (UserId, EmailAddress, UserName) = (userId, emailAddress, userName);
    }
}