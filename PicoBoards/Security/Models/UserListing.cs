using System;
using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Security.Models
{
    public sealed class UserListing : IModel
    {
        public int UserId { get; }

        public string UserName { get; }

        public string GroupName { get; }

        public string ProfileImageUrl { get; }

        public DateTime Created { get; }

        public DateTime LastActive { get; }

        public int PostCount { get; }

        public int TopicCount { get; }

        public UserListing(
            int userId,
            string userName,
            string groupName,
            string profileImageUrl,
            DateTime created,
            DateTime lastActive,
            int postCount,
            int topicCount)
        {
            UserId = userId;
            UserName = userName;
            GroupName = groupName;
            ProfileImageUrl = profileImageUrl;
            Created = created;
            LastActive = lastActive;
            PostCount = postCount;
            TopicCount = topicCount;
        }
    }

    public sealed class UserListingTable : ICollection<UserListing>
    {
        private readonly List<UserListing> list;

        public UserListingTable()
            => list = new List<UserListing>();

        public UserListingTable(IEnumerable<UserListing> collection)
            => list = new List<UserListing>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(UserListing item)
            => list.Add(item);

        public void Clear()
            => list.Clear();

        public bool Contains(UserListing item)
            => list.Contains(item);

        public void CopyTo(UserListing[] array, int arrayIndex)
            => list.CopyTo(array, arrayIndex);

        public IEnumerator<UserListing> GetEnumerator()
            => list.GetEnumerator();

        public bool Remove(UserListing item)
            => list.Remove(item);

        IEnumerator IEnumerable.GetEnumerator()
            => list.GetEnumerator();
    }
}