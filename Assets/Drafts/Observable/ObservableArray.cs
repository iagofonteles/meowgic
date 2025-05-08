using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
    public class ObservableArray<T> : IObservableList<T>
    {
        [SerializeField] private T[] array;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public T this[int index]
        {
            get => array[index];
            set
            {
                var old = array[index];
                array[index] = value;
                var args = new ChangedArgs(Action.Replace, value, old, index);
                CollectionChanged?.Invoke(this, args);
            }
        }

        public int Length => array.Length;
        public int Count => array.Length;

        public bool Contains(T item) => array.Contains(item);
        public int IndexOf(T item) => array.IndexOf(item);
        public void CopyTo(T[] other, int arrayIndex) => array.CopyTo(other, arrayIndex);

        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>)array).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public ObservableArray(int length) => array = new T[length];
        public ObservableArray(IEnumerable<T> items) => array = items.ToArray();

        public void Clear()
        {
            for (int i = 0; i < array.Length; i++)
                array[i] = default;

            var args = new ChangedArgs(Action.Reset);
            CollectionChanged?.Invoke(this, args);
        }

        public void Swap(int indexA, int indexB)
        {
            (array[indexA], array[indexB]) = (array[indexB], array[indexA]);
            var args = new ChangedArgs(Action.Replace, array[indexB], array[indexA], indexA);
            CollectionChanged?.Invoke(this, args);
            args = new ChangedArgs(Action.Replace, array[indexA], array[indexB], indexB);
            CollectionChanged?.Invoke(this, args);
        }
    }
}