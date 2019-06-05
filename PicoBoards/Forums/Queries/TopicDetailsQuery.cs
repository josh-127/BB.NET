
namespace PicoBoards.Forums.Queries
{
    public sealed class TopicDetailsQuery
    {
        public int TopicId { get; }

        public TopicDetailsQuery(int topicId)
            => TopicId = topicId;
    }
}