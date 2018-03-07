using System;
using System.Collections.Generic;
using System.Text;

namespace Sgk.Libs.Naybe
{
    /// <summary>
    /// Provides static methods for dictionaries.
    /// </summary>
    public static class IDictionaryExt
    {
        delegate bool tryGetValue<TKey, TValue>(TKey key, out TValue value);

        /// <summary>
        /// Gets the value associated with the specified key, or <see cref="Maybe.Null{T}"/> if the key isn't found in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Maybe<TValue> MayGetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
        {
            return _MayGetValue<TKey, TValue>(dictionary.TryGetValue, key);
        }
        /// <summary>
        /// Gets the value associated with the specified key, or <see cref="Maybe.Null{T}"/> if the key isn't found in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Maybe<TValue> MayGetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) where TValue : class
        {
            return _MayGetValue<TKey, TValue>(dictionary.TryGetValue, key);
        }

        /// <summary>
        /// Gets the value associated with the specified key, or <see cref="Maybe.Null{T}"/> if the key isn't found in the dictionary.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Maybe<TValue> MayGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key) where TValue : class
        {
            return _MayGetValue<TKey, TValue>(dictionary.TryGetValue, key);
        }


        private static Maybe<TValue> _MayGetValue<TKey, TValue>(tryGetValue<TKey, TValue> tryGetValue, TKey key) where TValue : class
        {
            TValue val;
            tryGetValue(key, out val);
            return new Maybe<TValue>(val);
        }
    }
}
