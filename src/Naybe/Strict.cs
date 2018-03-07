using System;
using System.Collections.Generic;
using System.Text;

namespace Sgk.Libs.Naybe
{
    /// <summary>
    /// Represents the wrapped object is NOT null.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Strict<T> where T: class
    {
        /// <summary>
        /// Gets the core object which isn't null.
        /// </summary>
        public T Val { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Strict{T}"/> structure.
        /// </summary>
        /// <param name="val"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Strict(T val)
        {
            if (val == null) throw new ArgumentNullException(nameof(val));

            this.Val = val;
        }

        /// <summary>
        /// Returns the string representation of the <see cref="Val"/> object.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Val.ToString();
        }

        /// <summary>
        /// Defines an implicit conversion of a <see cref="Strict{T}"/> instance to its underlying value.
        /// </summary>
        /// <param name="strict">an instance of <see cref="Strict{T}"/>.</param>
        public static implicit operator T(Strict<T> strict)
        {
            return strict.Val;
        }

        /// <summary>
        /// Creates a new <see cref="Strict{T}"/> instance with a specified value.
        /// </summary>
        /// <param name="val"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static explicit operator Strict<T>(T val)
        {
            return Strict.Of(val);
        }

        /// <summary>
        /// Determines whether a specified object is equal to this instance.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = (Strict<T>)obj;
            return this.Val.Equals(other.Val);
        }

        /// <summary>
        /// Gets a hash code for this instance.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Val.GetHashCode();
        }

        /// <summary>
        /// Compares two wrapped values for equality.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the values of left and right are equal; otherwise, false.</returns>
        public static bool operator ==(Strict<T> left, Strict<T> right)
        {
            return left.Val == right.Val;
        }

        /// <summary>
        /// Compares two wrapped values for inequality.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>true if the values of left and right are different; otherwise, false.</returns>
        public static bool operator !=(Strict<T> left, Strict<T> right)
        {
            return left.Val != right.Val;
        }
    }

    /// <summary>
    /// Defines the static method to construct <see cref="Strict{T}" /> structures.
    /// </summary>
    public static class Strict
    {
        /// <summary>
        /// Constructs the new instance of the <see cref="Strict{T}"/> structure.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="val"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static Strict<T> Of<T>(T val) where T: class
        {
            return new Strict<T>(val);
        }
    }
}
