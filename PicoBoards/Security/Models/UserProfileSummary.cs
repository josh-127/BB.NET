
namespace PicoBoards.Security.Models
{
    public sealed class UserProfileSummary
    {
        public int UserId { get; }

        public string UserName { get; }

        public string Signature { get; }

        public UserProfileSummary(int userId, string userName, string signature)
            => (UserId, UserName, Signature) = (userId, userName, signature);
    }
}