using System.Collections;
using System.Collections.Generic;

namespace PicoBoards.Forums.Models
{
    public sealed class CategoryRef
    {
        public int CategoryId { get; }

        public string Name { get; }

        public CategoryRef(int categoryId, string name)
            => (CategoryId, Name) = (categoryId, name);
    }

    public sealed class CategoryRefCollection : ICollection<CategoryRef>
    {
        private readonly List<CategoryRef> list;

        public CategoryRefCollection()
            => list = new List<CategoryRef>();

        public CategoryRefCollection(IEnumerable<CategoryRef> collection)
            => list = new List<CategoryRef>(collection);

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(CategoryRef item) => list.Add(item);
        public void Clear() => list.Clear();
        public bool Contains(CategoryRef item) => list.Contains(item);
        public void CopyTo(CategoryRef[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public IEnumerator<CategoryRef> GetEnumerator() => list.GetEnumerator();
        public bool Remove(CategoryRef item) => list.Remove(item);
        IEnumerator IEnumerable.GetEnumerator() => list.GetEnumerator();
    }
}