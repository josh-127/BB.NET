using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Forums.Models
{
    public sealed class CategoryListing
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }
    }

    public sealed class CategoryListingCollection : ICollection<CategoryListing>
    {
        private readonly List<CategoryListing> list;

        public CategoryListingCollection()
            => list = new List<CategoryListing>();

        public CategoryListingCollection(IEnumerable<CategoryListing> collection)
            => list = new List<CategoryListing>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(CategoryListing item)
            => list.Add(item);

        public void Clear()
            => list.Clear();

        public bool Contains(CategoryListing item)
            => list.Contains(item);

        public void CopyTo(CategoryListing[] array, int arrayIndex)
            => list.CopyTo(array, arrayIndex);

        public IEnumerator<CategoryListing> GetEnumerator()
            => list.GetEnumerator();

        public bool Remove(CategoryListing item)
            => list.Remove(item);

        IEnumerator IEnumerable.GetEnumerator()
            => list.GetEnumerator();
    }
}