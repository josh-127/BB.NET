
namespace PicoBoards.Security.Queries
{
    public sealed class PublicUserProfileQuery
    {
        public int UserId { get; }

        public PublicUserProfileQuery(int userId)
            => UserId = userId;
    }
}