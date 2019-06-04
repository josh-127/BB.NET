using System.ComponentModel.DataAnnotations;

namespace PicoBoards.Forums.Commands
{
    public sealed class RemoveTopicCommand : IValidatable
    {
        [Required]
        public int TopicId { get; }

        public RemoveTopicCommand(int topicId)
            => TopicId = topicId;
    }
}