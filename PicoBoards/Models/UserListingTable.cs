using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Models
{
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