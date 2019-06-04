using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Forums.Models
{
    public sealed class ForumListing
    {
        public int ForumId { get; }

        public string Name { get; }

        public string Description { get; }

        public string ImageUrl { get; }

        public ForumListing(int forumId, string name, string description, string imageUrl)
        {
            ForumId = forumId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
        }
    }

    public sealed class ForumListingCollection : ICollection<ForumListing>
    {
        private readonly List<ForumListing> list;

        public ForumListingCollection()
            => list = new List<ForumListing>();

        public ForumListingCollection(IEnumerable<ForumListing> collection)
            => list = new List<ForumListing>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(ForumListing item) => list.Add(item);
        public void Clear() => list.Clear();
        public bool Contains(ForumListing item) => list.Contains(item);
        public void CopyTo(ForumListing[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public IEnumerator<ForumListing> GetEnumerator() => list.GetEnumerator();
        public bool Remove(ForumListing item) => list.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}