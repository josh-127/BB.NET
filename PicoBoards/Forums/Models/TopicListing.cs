using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Forums.Models
{
    public sealed class TopicListing
    {
        public int TopicId { get; }

        public string Name { get; }

        public string Description { get; }

        public bool IsLocked { get; }

        public bool IsSticky { get; }

        public TopicListing(
            int topicId,
            string name,
            string description,
            bool isLocked,
            bool isSticky)
        {
            TopicId = topicId;
            Name = name;
            Description = description;
            IsLocked = isLocked;
            IsSticky = isSticky;
        }
    }

    public sealed class TopicListingCollection : ICollection<TopicListing>
    {
        private readonly List<TopicListing> list;

        public TopicListingCollection()
            => list = new List<TopicListing>();

        public TopicListingCollection(IEnumerable<TopicListing> collection)
            => list = new List<TopicListing>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(TopicListing item) => list.Add(item);
        public void Clear() => list.Clear();
        public bool Contains(TopicListing item) => list.Contains(item);
        public void CopyTo(TopicListing[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public IEnumerator<TopicListing> GetEnumerator() => list.GetEnumerator();
        public bool Remove(TopicListing item) => list.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}