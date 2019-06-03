
namespace PicoBoards.Security.Queries
{
    public sealed class UserEmailAddressQuery
    {
        public int UserId { get; }

        public UserEmailAddressQuery(int userId)
            => UserId = userId;
    }
}