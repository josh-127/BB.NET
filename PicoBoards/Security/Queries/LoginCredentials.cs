
namespace PicoBoards.Security.Queries
{
    public sealed class LoginCredentials
    {
        public string UserName { get; }

        public string Password { get; }

        public LoginCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}