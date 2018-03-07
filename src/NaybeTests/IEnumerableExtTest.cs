using NUnit.Framework;
using Sgk.Libs.Naybe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NaybeTests
{
    [TestFixture]
    class IEnumerableExtTest
    {
        private static IEnumerable<TestCaseData> CollectNotNullValuesSource
        {
            get
            {
                yield return new TestCaseData(new Maybe<string>[] { }).Returns(Enumerable.Empty<string>());

                yield return new TestCaseData(new[]
                {
                    new Maybe<string>(),
                    new Maybe<string>(""),
                    new Maybe<string>("ほげ"),
                    Maybe.Null<string>(),
                    Maybe.Just(" abc "),
                }).Returns(new[] { "", "ほげ", " abc " });
            }
        }

        [TestCaseSource(nameof(CollectNotNullValuesSource))]
        public IEnumerable<string> CollectNotNullValues(Maybe<string>[] original)
        {
            return original.Values();
        }

        private static IEnumerable<TestCaseData> FindFirstElementSource
        {
            get
            {
                yield return new TestCaseData(new string[0], null).Returns(Maybe.Null<string>());
                yield return new TestCaseData(new[] { "ab", "abc", "abcd", "ddd" }, new Func<string, bool>(s => s.Length == 3))
                                    .Returns(Maybe.Just("abc"));
                yield return new TestCaseData(new[] { "ab", "abc", "abcd", "ddd" }, new Func<string, bool>(s => s.Length == 1))
                                    .Returns(Maybe.Null<string>());
            }
        }

        [TestCaseSource(nameof(FindFirstElementSource))]
        public Maybe<string> FindFirstElement(IEnumerable<string> collection, Func<string, bool> predicate)
        {
            if (predicate == null)
            {
                return collection.MaybeFirst();
            }
            else
            {
                return collection.MaybeFirst(predicate);
            }
        }
    }
}
