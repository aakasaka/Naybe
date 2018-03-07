using System;
using System.Collections.Generic;
using System.Text;

namespace Sgk.Libs.Naybe
{
    /// <summary>
    /// Represents the wrapped object maybe null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Maybe<T> where T : class
    {
        private readonly T core;

        /// <summary>
        /// Gets the wrapped object which isn't null.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throws if the wrapped object is null.</exception>
        public T Val
        {
            get
            {
                if (core == null) throw new InvalidOperationException();
                return core;
            }
        }
        /// <summary>
        /// Gets whether the wrapped object is non-null.
        /// </summary>
        public bool HasVal
        {
            get
            {
                return core != null;
            }
        }
        /// <summary>
        /// Gets whether the wrapped object is null.
        /// </summary>
        public bool IsNull
        {
            get
            {
                return core == null;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Maybe{T}"/> structure to the specified object.
        /// </summary>
        /// <param name="value"></param>
        public Maybe(T value)
            : this()
        {
            this.core = value;
        }

        /// <summary>
        /// Returns the wrapped object if it's not null, otherwise the specified value.
        /// </summary>
        /// <param name="ifNull"></param>
        /// <returns></returns>
        public T Or(T ifNull)
        {
            return core ?? ifNull;
        }

        /// <summary>
        /// Executes the specified action with this value, when the wrapped object is not null.
        /// </summary>
        /// <param name="action"></param>
        public void IfPresent(Action<T> action)
        {
            if (core != null) action(core);
        }

        /// <summary>
        /// Executes the specified action and returns the result value, when the wrapped object is not null.
        /// Otherwise returns the default value of the type.
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public TRet IfPresent<TRet>(Func<T, TRet> func)
        {
            if (core == null) return default(TRet);
            return func(core);
        }

        /// <summary>
        /// Returns the string representation of the wrapped object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (core == null) return "";
            return core.ToString();
        }

        /// <summary>
        /// Determines whether a specified object is equal to this instance.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Maybe<T>)) return false;

            var other = (Maybe<T>)obj;

            if (this.core == null) return other.core == null;

            return this.core.Equals(other.core);
        }
        /// <summary>
        /// Gets a hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            if (core == null) return 0;

            return core.GetHashCode();
        }

        /// <summary>
        /// Creates a new <see cref="Maybe{T}"/> instance with the non-null object or null.
        /// </summary>
        /// <param name="val"></param>
        public static implicit operator Maybe<T>(T val)
        {
            return new Maybe<T>(val);
        }
        /// <summary>
        /// Defines an explicit conversion of a <see cref="Maybe{T}"/> instance to its wrapped object.
        /// </summary>
        /// <param name="maybe"></param>
        public static explicit operator T(Maybe<T> maybe)
        {
            if (!maybe.HasVal) throw new InvalidCastException();

            return maybe.Val;
        }

        /// <summary>
        /// Compares two wrapped values for equality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Maybe<T> left, Maybe<T> right)
        {
            if (left.IsNull) return right.IsNull;

            if (right.IsNull) return false;

            return left.Val == right.Val;
        }

        /// <summary>
        /// Compares two wrapped values for inequality.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Maybe<T> left, Maybe<T> right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    /// Defines static methods to create a new instance of <see cref="Maybe{T}"/> structure.
    /// </summary>
    public static class Maybe
    {
        /// <summary>
        /// Creates a new instance of <see cref="Maybe{T}"/> with null value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Maybe<T> Null<T>() where T : class
        {
            return new Maybe<T>();
        }
        /// <summary>
        /// Creates a new instance of <see cref="Maybe{T}"/> with the non-null value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="notNullVal"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Maybe<T> Just<T>(T notNullVal) where T : class
        {
            if (notNullVal == null) throw new ArgumentNullException(nameof(notNullVal));

            return new Maybe<T>(notNullVal);
        }
    }
}
