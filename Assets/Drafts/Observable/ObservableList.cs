using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

namespace Utility
{
    using Action = NotifyCollectionChangedAction;
    using ChangedArgs = NotifyCollectionChangedEventArgs;

    /// <summary>
    /// Made this cause ObservableCollection inst serializable
    /// </summary>
    [Serializable]
    public class ObservableList<T> : IList<T>, INotifyCollectionChanged
    {
        [SerializeField] private List<T> _list;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public T this[int index]
        {
            get => _list[index];
            set => _list[index] = value;
        }

        public int Count => _list.Count;
        public bool IsReadOnly => ((IList<T>)_list).IsReadOnly;

        public bool Contains(T item) => _list.Contains(item);
        public int IndexOf(T item) => _list.IndexOf(item);
        public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);
        public void Add(T item) => Insert(Count, item);

        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_list).GetEnumerator();

        public ObservableList() => _list = new();
        public ObservableList(IEnumerable<T> items) => _list = new(items);

        public void Insert(int index, T item)
        {
            _list.Insert(index, item);
            var args = new ChangedArgs(Action.Add, item, index);
            CollectionChanged?.Invoke(this, args);
        }

        public void Clear()
        {
            _list.Clear();
            var args = new ChangedArgs(Action.Reset);
            CollectionChanged?.Invoke(this, args);
        }

        public bool Remove(T item)
        {
            var index = _list.IndexOf(item);
            if (index < 0) return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            var item = _list[index];
            _list.RemoveAt(index);
            var args = new ChangedArgs(Action.Remove, item, index);
            CollectionChanged?.Invoke(this, args);
        }
    }
}