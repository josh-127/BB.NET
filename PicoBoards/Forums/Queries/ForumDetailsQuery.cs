
namespace PicoBoards.Forums.Queries
{
    public sealed class ForumDetailsQuery
    {
        public int ForumId { get; }

        public ForumDetailsQuery(int forumId)
            => ForumId = forumId;
    }
}