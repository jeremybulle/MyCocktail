using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCocktail.Domain.Helper
{
    public static class EnumerableHelper
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null || enumerable.Count() < 1)
            {
                return true;
            }

            return false;
        }

        public static void PurgeNullValue<TKey, TValue>(this IDictionary<TKey, TValue> dict)
        {
            if (dict != null)
            {
                foreach (var entry in dict)
                {
                    if (entry.Value == null)
                    {
                        dict.Remove(entry.Key);
                    }
                }
            }
        }

        public static void PurgeEmptyAndNullValue<Tkey>(this IDictionary<Tkey, string> dict)
        {
            if (dict != null)
            {
                foreach (var entry in dict)
                {
                    if (entry.Value == null || entry.Value == "")
                    {
                        dict.Remove(entry.Key);
                    }
                }
            }
        }

        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            if (a == null)
            {
                throw new ArgumentNullException("The collection which probably contain the other can not be null");
            }
            if (b == null)
            {
                return true;
            }
            return !b.Except(a).Any();
        }
    }
}
