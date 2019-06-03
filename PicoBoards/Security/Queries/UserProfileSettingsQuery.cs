
namespace PicoBoards.Security.Queries
{
    public sealed class UserProfileSettingsQuery
    {
        public int UserId { get; }

        public UserProfileSettingsQuery(int userId)
            => UserId = userId;
    }
}