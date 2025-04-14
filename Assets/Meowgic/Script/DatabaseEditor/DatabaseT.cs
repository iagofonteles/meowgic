using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Meowgic
{
    [Serializable]
    public class Database<T> : IDatabase, IEnumerable<T> where T : IDatabaseItem
    {
        [SerializeField, HideInInspector] private string name;
        [SerializeField, NonReorderable] private List<T> items;
        private Dictionary<string, T> _map;
        public Type Type => typeof(T);

        public T this[int index] => Get(index, false);
        public T this[string name] => Get(name, false);
        public int Count => items.Count - 1;

        public T Get(string name, bool throwException = true)
        {
            _map ??= items.ToDictionary(i => i.name, i => i);
            return _map.TryGetValue(name, out var v)
                ? v
                : throwException
                    ? throw new Exception($"{typeof(T).Name} {name} not found")
                    : default;
        }

        public T Get(int index, bool throwException = true)
        {
            if (index < 1 || index >= items.Count)
                return throwException ? throw new IndexOutOfRangeException(index.ToString()) : default;
            return items[index];
        }

        public Database(IEnumerable newItems)
        {
            name = typeof(T).Name;
            _map = null;
            items = new(newItems.OfType<T>());
        }

        IDatabaseItem IDatabase.Get(int index) => Get(index, false);
        IDatabaseItem IDatabase.Get(string name) => Get(name, false);
        public IEnumerator<T> GetEnumerator() => items.Skip(1).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => items.Skip(1).GetEnumerator();
    }
}