using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Meowgic
{
    [CreateAssetMenu(menuName = "Meowgic/Database")]
    public partial class Database : ScriptableObject, IEnumerable<IDatabaseItem>
    {
        [SerializeField] private string searchFolder = "Assets/Meowgic/Database";
        [SerializeReference, NonReorderable] private IDatabase[] databases;

        public IEnumerable<T> GetAll<T>() where T : IDatabaseItem => Map[typeof(T)].OfType<T>();
        public T Get<T>(int itemIndex) where T : IDatabaseItem => (T)Map[typeof(T)].Get(itemIndex);
        public T Get<T>(string itemName) where T : IDatabaseItem => (T)Map[typeof(T)].Get(itemName);

        private Dictionary<Type, IDatabase> _map;
        private Dictionary<Type, IDatabase> Map => _map ??= databases.ToDictionary(i => i.Type, i => i);

        public IEnumerator<IDatabaseItem> GetEnumerator()
            => Map.Values.SelectMany(v => v.OfType<IDatabaseItem>()).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}