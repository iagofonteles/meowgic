using System;
using System.Collections.Generic;
using UnityEngine;

namespace Drafts
{
    public delegate void ValueChangedHandler<in T>(T oldValue, T newValue);

    public interface IObservable
    {
        object Value { get; }
        event Action<object> OnChanged;
    }

    [Serializable]
    public class Observable<T> : IObservable
    {
        [SerializeField] private T _value;

        public Observable() { }

        public Observable(T value) => _value = value;

        object IObservable.Value => _value;

        public T Value
        {
            get => _value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(value, _value))
                    return;

                var old = _value;
                _value = value;
                OnChanged?.Invoke(value);
                OnValueChanged?.Invoke(old, value);
            }
        }

        public event Action<object> OnChanged;
        public event ValueChangedHandler<T> OnValueChanged;
    }
}