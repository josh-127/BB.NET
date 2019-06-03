
namespace PicoBoards.Security.Authentication
{
    public class LoginToken
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public LoginToken() { }

        public LoginToken(int userId, string userName)
            => (UserId, UserName) = (userId, userName);
    }
}