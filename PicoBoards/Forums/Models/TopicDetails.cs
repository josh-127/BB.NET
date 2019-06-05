using System;

namespace PicoBoards.Forums.Models
{
    public sealed class TopicDetails
    {
        public int TopicId { get; }

        public string Name { get; }

        public string Description { get; }

        public DateTime Created { get; }

        public bool IsLocked { get; }

        public bool IsSticky { get; }

        public PostListingCollection Posts { get; }

        public TopicDetails(
            int topicId,
            string name,
            string description,
            DateTime created,
            bool isLocked,
            bool isSticky,
            PostListingCollection posts)
        {
            TopicId = topicId;
            Name = name;
            Description = description;
            Created = created;
            IsLocked = isLocked;
            IsSticky = isSticky;
            Posts = posts;
        }
    }
}