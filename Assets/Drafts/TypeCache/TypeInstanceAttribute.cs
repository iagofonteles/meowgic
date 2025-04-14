using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Drafts
{
    public class TypeInstanceAttribute : PropertyAttribute { }

    [Serializable]
    public class TypeInstances<T> : IReadOnlyList<T>, IList<T>
    {
        [SerializeReference] public List<T> list = new();

        public int Count => list.Count;
        public bool IsReadOnly => ((IList)list).IsReadOnly;
        
        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }
        
        public int IndexOf(T item) => list.IndexOf(item);
        public void Insert(int index, T item) => list.Insert(index, item);
        public void RemoveAt(int index) => list.RemoveAt(index);
        public void Add(T item) => list.Add(item);
        public void AddRange(IEnumerable<T> items) => list.AddRange(items);
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public bool Remove(T item) => list.Remove(item);
        public void Clear() => list.Clear();
        public bool Contains(T item) => list.Contains(item);

        public TypeInstances(IEnumerable<T> elements) => list.AddRange(elements);
        public TypeInstances() { }

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}