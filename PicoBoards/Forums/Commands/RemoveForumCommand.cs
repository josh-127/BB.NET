
namespace PicoBoards.Forums.Commands
{
    public sealed class RemoveForumCommand : IValidatable
    {
        public int ForumId { get; }

        public RemoveForumCommand(int forumId)
            => ForumId = forumId;
    }
}