using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Forums.Commands
{
    public sealed class AddTopicCommand : IValidatable
    {
        [Required]
        public int ForumId { get; }

        [Required]
        public int AuthorUserId { get; }

        [Required]
        public string Name { get; }

        public string Description { get; }

        public AddTopicCommand(int forumId, int authorUserId, string name, string description)
        {
            ForumId = forumId;
            AuthorUserId = authorUserId;
            Name = name;
            Description = description;
        }
    }
}