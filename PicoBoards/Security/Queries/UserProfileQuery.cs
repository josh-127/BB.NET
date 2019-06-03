
namespace PicoBoards.Security.Queries
{
    public sealed class UserProfileQuery
    {
        public int UserId { get; }

        public UserProfileQuery(int userId)
            => UserId = userId;
    }
}