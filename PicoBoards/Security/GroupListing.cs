using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Security
{
    public class GroupListing
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }

    public sealed class GroupListingTable : ICollection<GroupListing>
    {
        private readonly List<GroupListing> list;

        public GroupListingTable()
            => list = new List<GroupListing>();

        public GroupListingTable(IEnumerable<GroupListing> collection)
            => list = new List<GroupListing>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(GroupListing item)
            => list.Add(item);

        public void Clear()
            => list.Clear();

        public bool Contains(GroupListing item)
            => list.Contains(item);

        public void CopyTo(GroupListing[] array, int arrayIndex)
            => list.CopyTo(array, arrayIndex);

        public IEnumerator<GroupListing> GetEnumerator()
            => list.GetEnumerator();

        public bool Remove(GroupListing item)
            => list.Remove(item);

        IEnumerator IEnumerable.GetEnumerator()
            => list.GetEnumerator();
    }
}