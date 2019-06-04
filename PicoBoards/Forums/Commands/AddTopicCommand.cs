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

        [Required]
        public string OpeningPostBody { get; }

        public bool FormattingEnabled { get; }

        public bool SmiliesEnabled { get; }

        public bool ParseUrls { get; }

        public bool AttachSignature { get; }

        public AddTopicCommand(
            int forumId,
            int authorUserId,
            string name,
            string description,
            string openingPostBody,
            bool formattingEnabled,
            bool smiliesEnabled,
            bool parseUrls,
            bool attachSignature)
        {
            ForumId = forumId;
            AuthorUserId = authorUserId;
            Name = name;
            Description = description;
            OpeningPostBody = openingPostBody;
            FormattingEnabled = formattingEnabled;
            SmiliesEnabled = smiliesEnabled;
            ParseUrls = parseUrls;
            AttachSignature = attachSignature;
        }
    }
}