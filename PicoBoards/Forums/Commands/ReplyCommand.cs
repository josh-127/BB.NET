using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Forums.Commands
{
    public sealed class ReplyCommand : IValidatable
    {
        [Required]
        public int TopicId { get; }

        [Required]
        public int UserId { get; }

        [Required]
        public string Name { get; }

        [Required]
        public string Body { get; }

        public bool FormattingEnabled { get; }

        public bool SmiliesEnabled { get; }

        public bool ParseUrls { get; }

        public bool AttachSignature { get; }

        public ReplyCommand(
            int topicId,
            int userId,
            string name,
            string body,
            bool formattingEnabled,
            bool smiliesEnabled,
            bool parseUrls,
            bool attachSignature)
        {
            TopicId = topicId;
            UserId = userId;
            Name = name;
            Body = body;
            FormattingEnabled = formattingEnabled;
            SmiliesEnabled = smiliesEnabled;
            ParseUrls = parseUrls;
            AttachSignature = attachSignature;
        }
    }
}