using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sgk.Libs.Naybe
{
    /// <summary>
    /// Provides static methods for collections and <see cref="Maybe{T}"/>.
    /// </summary>
    public static class IEnumerableExt
    {
        /// <summary>
        /// Picks up non-null values from the collection of <see cref="Maybe{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<T> Values<T>(this IEnumerable<Maybe<T>> collection) where T: class
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return collection.Where(m => m.HasVal).Select(m => m.Val);
        }
        /// <summary>
        /// Returns the first element of the sequense, or <see cref="Maybe.Null{T}"/> if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> collection) where T : class
        {
            return MaybeFirst(collection, _ => true);
        }
        /// <summary>
        /// Returns the first element of the sequense that satisfies a condition, 
        /// or <see cref="Maybe.Null{T}"/> if no such element is found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Maybe<T> MaybeFirst<T>(this IEnumerable<T> collection, Func<T, bool> predicate) where T: class
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return collection.FirstOrDefault(predicate);
        }
        /// <summary>
        /// Returns the last element of the sequence, or <see cref="Maybe.Null{T}"/> if the sequence contains no elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static Maybe<T> MaybeLast<T>(this IEnumerable<T> collection) where T : class
        {
            return MaybeLast(collection, _ => true);
        }
        /// <summary>
        /// Returns the last element of the sequence that satisfies a condition,
        /// or <see cref="Maybe.Null{T}"/> if no such element is found.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static Maybe<T> MaybeLast<T>(this IEnumerable<T> collection, Func<T, bool> predicate) where T: class
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return collection.LastOrDefault(predicate);
        }

        /// <summary>
        /// Returns the element at a specified index int a sequence, 
        /// or <see cref="Maybe.Null{T}"/> if the index if out of range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static Maybe<T> MaybeAt<T>(this IEnumerable<T> collection, int index) where T: class
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return collection.ElementAtOrDefault(index);
        }
    }
}
