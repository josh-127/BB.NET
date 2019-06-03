
namespace PicoBoards.Security.Queries
{
    public sealed class UserNameQuery
    {
        public int UserId { get; }

        public UserNameQuery(int userId)
            => UserId = userId;
    }
}