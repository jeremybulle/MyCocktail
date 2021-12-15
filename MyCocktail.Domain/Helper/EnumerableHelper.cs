using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCocktail.Domain.Helper
{
    public static class EnumerableHelper
    {
        /// <summary>
        /// Allow to know if an <see langword="IEnumerable"/> is null or empty
        /// </summary>
        /// <typeparam name="T">Type Contains in enumarable</typeparam>
        /// <param name="enumerable">Collection to test</param>
        /// <returns><see langword="true"/> if enumerable is null or not contains element, <see langword="false"/></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable == null || !enumerable.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remove all entry withn <see langword="null"/> value
        /// </summary>
        /// <typeparam name="TKey">key's type of the couple key/value</typeparam>
        /// <typeparam name="TValue">value's type of the couple key/value</typeparam>
        /// <param name="dict"><see langword="IDictionary"/> on wich this is applied</param>
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

        /// <summary>
        /// Remove all entry where the value is null or empty string
        /// </summary>
        /// <typeparam name="Tkey">Key's type of the couple key/string</typeparam>
        /// <param name="dict"><see langword="IDictionary"/> on wich this is applied</param>
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

        /// <summary>
        /// Allow to know if an <see langword="IEnumerable"/> A contain all elements of an <see langword="IEnumerable"/> B
        /// </summary>
        /// <typeparam name="T">Elements' Type</typeparam>
        /// <param name="collectionToTest">Collection tested</param>
        /// <param name="itemsShoudlBeIncluded">Collection containing element searched in the collection to test</param>
        /// <returns><see langword="true"/> if <see langword="IEnumerable"/> A contain all elements of an <see langword="IEnumerable"/> B, <see langword="false"/> if is missing element()</returns>
        public static bool ContainsAllItems<T>(this IEnumerable<T> A, IEnumerable<T> B)
        {
            if (A == null)
            {
                throw new ArgumentNullException(nameof(A), $"The collection which probably contain the other can not be null");
            }
            if (B == null)
            {
                return true;
            }
            return !B.Except(A).Any();
        }
    }
}
