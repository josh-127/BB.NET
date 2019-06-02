using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PicoBoards
{
    public sealed class ValidationResultCollection : ICollection<ValidationResult>
    {
        private readonly List<ValidationResult> list = new List<ValidationResult>();

        public bool IsValid => list.Count == 0;

        public int Count => list.Count;

        public bool IsReadOnly => false;

        public void Add(ValidationResult item)
            => list.Add(item);

        public void Clear()
            => list.Clear();

        public bool Contains(ValidationResult item)
            => list.Contains(item);

        public void CopyTo(ValidationResult[] array, int arrayIndex)
            => list.CopyTo(array, arrayIndex);

        public IEnumerator<ValidationResult> GetEnumerator()
            => list.GetEnumerator();

        public bool Remove(ValidationResult item)
            => list.Remove(item);

        IEnumerator IEnumerable.GetEnumerator()
            => list.GetEnumerator();
    }
}