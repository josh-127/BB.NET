using System.Collections.Generic;
using System.Linq;

namespace BBNet.Data
{
    public class TopicService : ITopicService
    {
        private readonly BBNetDbContext context;

        public TopicService(BBNetDbContext context)
            => this.context = context;

        public Topic GetTopicById(int topicId)
            => context.Topics
                .Where(t => t.Id == topicId)
                .SingleOrDefault();

        public IEnumerable<Topic> GetAllTopics()
            => context.Topics;

        public IEnumerable<Topic> GetTopicsByForumId(int forumId)
            => context.Topics.Where(t => t.Forum.Id == forumId);

        public void AddTopic(Topic topic, Post openingPost, Forum forum)
        {
            openingPost.Topic = topic;
            topic.Forum = forum;

            context.Topics.Add(topic);
            context.Posts.Add(openingPost);

            context.SaveChanges();
        }
    }
}