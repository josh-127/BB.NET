
namespace PicoBoards.Security.Authentication.Queries
{
    public sealed class LoginCredentials
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public LoginCredentials() { }

        public LoginCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}