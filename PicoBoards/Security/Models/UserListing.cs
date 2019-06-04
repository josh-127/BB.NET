using System;
using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Security.Models
{
    public sealed class UserListing
    {
        public int UserId { get; }

        public string UserName { get; }

        public string GroupName { get; }

        public DateTime Created { get; }

        public DateTime LastActive { get; }

        public UserListing(
            int userId,
            string userName,
            string groupName,
            DateTime created,
            DateTime lastActive)
        {
            UserId = userId;
            UserName = userName;
            GroupName = groupName;
            Created = created;
            LastActive = lastActive;
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