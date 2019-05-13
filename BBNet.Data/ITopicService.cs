using System.Collections.Generic;

namespace BBNet.Data
{
    public interface ITopicService
    {
        Topic GetTopicById(int topicId);

        IEnumerable<Topic> GetAllTopics();

        IEnumerable<Topic> GetTopicsByForumId(int forumId);

        void AddTopic(Topic topic);
    }
}