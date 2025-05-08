using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Drafts
{
    public delegate void ValueChangedHandler<in T>(T oldValue, T newValue);

    public interface IObservable
    {
        object Value { get; }
        event Action<object> OnChanged;
    }

    public interface IObservable<out T> : IObservable
    {
        new T Value { get; }
        event Action<T> OnValueChanged;
        event ValueChangedHandler<T> OnValueChanged2;
    }

    [Serializable]
    public class Observable<T> : IObservable<T>, IDisposable
    {
        [SerializeField] private T value;

        public Observable() { }

        public Observable(T value) => this.value = value;

        object IObservable.Value => value;

        public T Value
        {
            get => value;
            set
            {
                if (EqualityComparer<T>.Default.Equals(value, this.value))
                    return;

                var old = this.value;
                this.value = value;
                OnChanged?.Invoke(value);
                OnValueChanged?.Invoke(value);
                OnValueChanged2?.Invoke(old, value);
            }
        }

        public event Action<object> OnChanged;
        public event Action<T> OnValueChanged;
        public event ValueChangedHandler<T> OnValueChanged2;

        public void Dispose()
        {
            OnChanged = null;
            OnValueChanged = null;
            OnValueChanged2 = null;
        }
    }
}