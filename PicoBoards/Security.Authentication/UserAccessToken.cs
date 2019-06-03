
namespace PicoBoards.Security.Authentication
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