using System;
using System.Collections.Generic;
using System.Linq;

namespace Meowgic.Match
{
    public static class CardPoolExtensions
    {
        /// <summary>Reorganize elements in a random order.</summary>
        public static void Shuffle<T>(this List<T> l)
        {
            for (var n = 0; n < l.Count; n++) l.Swap(n, UnityEngine.Random.Range(0, l.Count));
        }

        /// <summary>Swap _values of index n with m.</summary>
        public static void Swap<T>(this List<T> l, int n, int m)
        {
            (l[n], l[m]) = (l[m], l[n]);
        }

        public static T Random<T>(this IReadOnlyList<T> list) => Random(list, 1).FirstOrDefault();

        /// <summary>Get random _values from the array. They are still ordered though.</summary>
        public static List<T> Random<T>(this IReadOnlyList<T> list, int num)
        {
            num = Math.Min(num, list.Count);
            var result = new List<T>();
            if (num == 0) return result;

            for (var i = 0; i < list.Count; i++)
                if (UnityEngine.Random.value <= (num - result.Count) / (float)(list.Count - i))
                    result.Add(list[i]);
            return result;
        }
    }
}