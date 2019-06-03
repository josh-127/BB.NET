
namespace PicoBoards.Security.Authentication.Models
{
    public class UserAccessToken
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public UserAccessToken() { }

        public UserAccessToken(int userId, string userName)
            => (UserId, UserName) = (userId, userName);
    }
}