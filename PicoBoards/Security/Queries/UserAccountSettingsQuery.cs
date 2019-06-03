
namespace PicoBoards.Security.Queries
{
    public sealed class UserAccountSettingsQuery
    {
        public int UserId { get; }

        public UserAccountSettingsQuery(int userId)
            => UserId = userId;
    }
}