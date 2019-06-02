
namespace PicoBoards.Models
{
    public sealed class LoginResult : Result<LoginResult, LoginToken, ValidationResultCollection>
    {
        public LoginResult(LoginToken value) : base(value) { }
        public LoginResult(ValidationResultCollection value) : base(value) { }
    }
}