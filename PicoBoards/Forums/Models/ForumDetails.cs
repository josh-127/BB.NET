using System;

namespace PicoBoards.Forums.Models
{
    public sealed class ForumDetails
    {
        public int ForumId { get; }

        public string Name { get; }

        public string Description { get; }

        public DateTime Created { get; }

        public TopicListingCollection Topics { get; }

        public ForumDetails(
            int forumId,
            string name,
            string description,
            DateTime created,
            TopicListingCollection topics)
        {
            ForumId = forumId;
            Name = name;
            Description = description;
            Created = created;
            Topics = topics;
        }
    }
}