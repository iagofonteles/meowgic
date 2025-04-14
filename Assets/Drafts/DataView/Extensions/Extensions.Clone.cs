using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Drafts.DataView
{
    public static partial class DataViewExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clone<T>(this T view) where T : DataView
        {
            var clone = UnityEngine.Object.Instantiate(view, view.transform.parent);
            clone.gameObject.SetActive(true);
            return clone;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Clone<T>(this T view, object data) where T : DataView
        {
            var clone = Clone(view);
            clone.SetData(data);
            return clone;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> CloneMany<T>(this T view, IEnumerable data) where T : DataView
        {
            var result = new List<T>();
            foreach (var item in data)
                result.Add(view.Clone(item));
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TrySetData(this DataView view, object data)
        {
            if (view) view.SetData(data);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void TrySetData<T>(this DataView<T> view, T data)
        {
            if (view) view.Data = data;
        }
    }
}