using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drafts
{
    using Action = NotifyCollectionChangedAction;
    using ChangedArgs = NotifyCollectionChangedEventArgs;

    /// <summary>
    /// Made this cause ObservableCollection inst serializable
    /// </summary>
    [Serializable]
    public class ObservableList<T> : IList<T>, IReadOnlyList<T>, INotifyCollectionChanged
    {
        [SerializeField] private List<T> list;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public T this[int index]
        {
            get => list[index];
            set => list[index] = value;
        }

        public int Count => list.Count;
        public bool IsReadOnly => ((IList<T>)list).IsReadOnly;

        public bool Contains(T item) => list.Contains(item);
        public int IndexOf(T item) => list.IndexOf(item);
        public void CopyTo(T[] array, int arrayIndex) => list.CopyTo(array, arrayIndex);
        public void Add(T item) => Insert(Count, item);

        public IEnumerator<T> GetEnumerator() => list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)list).GetEnumerator();

        public ObservableList() => list = new();
        public ObservableList(IEnumerable<T> items) => list = new(items);

        public void Insert(int index, T item)
        {
            list.Insert(index, item);
            var args = new ChangedArgs(Action.Add, item, index);
            CollectionChanged?.Invoke(this, args);
        }

        public void Clear()
        {
            list.Clear();
            var args = new ChangedArgs(Action.Reset);
            CollectionChanged?.Invoke(this, args);
        }

        public bool Remove(T item)
        {
            var index = list.IndexOf(item);
            if (index < 0) return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            var args = new ChangedArgs(Action.Remove, item, index);
            CollectionChanged?.Invoke(this, args);
        }

        public void Swap(int indexA, int indexB)
        {
            (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
            var args = new ChangedArgs(Action.Replace, list[indexB], list[indexA], indexA);
            CollectionChanged?.Invoke(this, args);
            args = new ChangedArgs(Action.Replace, list[indexA], list[indexB], indexB);
            CollectionChanged?.Invoke(this, args);
        }
    }
}