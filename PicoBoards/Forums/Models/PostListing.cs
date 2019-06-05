using System;
using System.Collections;
using System.Collections.Generic;
using PicoBoards.Security.Models;

namespace PicoBoards.Forums.Models
{
    public sealed class PostListing
    {
        public int PostId { get; }

        public string Name { get; }

        public string Body { get; }

        public DateTime Created { get; }

        public DateTime Modified { get; }

        public bool FormattingEnabled { get; }

        public bool SmiliesEnabled { get; }

        public bool ParseUrls { get; }

        public bool AttachSignature { get; }

        public UserProfileSummary Author { get; }

        public PostListing(
            int postId,
            string name,
            string body,
            DateTime created,
            DateTime modified,
            bool formattingEnabled,
            bool smiliesEnabled,
            bool parseUrls,
            bool attachSignature,
            UserProfileSummary author)
        {
            PostId = postId;
            Name = name;
            Body = body;
            Created = created;
            Modified = modified;
            FormattingEnabled = formattingEnabled;
            SmiliesEnabled = smiliesEnabled;
            ParseUrls = parseUrls;
            AttachSignature = attachSignature;
            Author = author;
        }
    }

    public sealed class PostListingCollection : ICollection<PostListing>
    {
        private readonly List<PostListing> list;

        public PostListingCollection()
            => list = new List<PostListing>();

        public PostListingCollection(IEnumerable<PostListing> collection)
            => list = new List<PostListing>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(PostListing item) => list.Add(item);
        public void Clear() => list.Clear();
        public bool Contains(PostListing item) => list.Contains(item);
        public void CopyTo(PostListing[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public IEnumerator<PostListing> GetEnumerator() => list.GetEnumerator();
        public bool Remove(PostListing item) => list.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}