using System.Collections.Generic;

namespace Drafts
{
    public static class LinqExtension
    {
        public static int IndexOf<T>(this IEnumerable<T> ie, T value)
        {
            var index = 0;
            foreach (var v in ie)
            {
                if (EqualityComparer<T>.Default.Equals(v, value))
                    return index;
                index++;
            }

            return -1;
        }
    }
}