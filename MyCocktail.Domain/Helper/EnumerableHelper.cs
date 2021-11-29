using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            foreach (var entry in dict)
            {
                if (entry.Value == null)
                {
                    dict.Remove(entry.Key);
                }
            }
        }

        public static void PurgeEmptyAndNullValue<Tkey>(this IDictionary<Tkey, string> dict)
        {
            foreach (var entry in dict)
            {
                if (entry.Value == null || entry.Value == "")
                {
                    dict.Remove(entry.Key);
                }
            }
        }

        public static bool ContainsAllItems<T>(this IEnumerable<T> a, IEnumerable<T> b)
        {
            return !b.Except(a).Any();
        }
    }
}
