using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Forums.Models
{
    public sealed class CategoryDetails
    {
        public int CategoryId { get; }

        public string Name { get; }

        public ForumListingCollection Forums { get; }

        public CategoryDetails(int categoryId, string name, ForumListingCollection forums)
            => (CategoryId, Name, Forums) = (categoryId, name, forums);
    }

    public sealed class CategoryDetailsCollection : ICollection<CategoryDetails>
    {
        private readonly List<CategoryDetails> list;

        public CategoryDetailsCollection()
            => list = new List<CategoryDetails>();

        public CategoryDetailsCollection(IEnumerable<CategoryDetails> collection)
            => list = new List<CategoryDetails>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(CategoryDetails item) => list.Add(item);
        public void Clear() => list.Clear();
        public bool Contains(CategoryDetails item) => list.Contains(item);
        public void CopyTo(CategoryDetails[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public IEnumerator<CategoryDetails> GetEnumerator() => list.GetEnumerator();
        public bool Remove(CategoryDetails item) => list.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}