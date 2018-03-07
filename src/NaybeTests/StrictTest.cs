using NUnit.Framework;
using Sgk.Libs.Naybe;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaybeTests
{
    [TestFixture]
    class StrictTest
    {
        [TestCase("")]
        [TestCase("日本語")]
        [TestCase("abc AAA")]
        public void KeepStringValue(string val)
        {
            var strict = Strict.Of(val);

            Assert.That(strict.Val, Is.EqualTo(val));
        }

        [Test]
        public void ThrowExceptionWhenValueIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Strict<string>(null));
            Assert.Throws<ArgumentNullException>(() => Strict.Of<string>(null));
        }


        private static IEnumerable<TestCaseData> EqualsSource
        {
            get
            {
                yield return new TestCaseData(Strict.Of("abc"), Strict.Of("abc")).Returns(true);
                yield return new TestCaseData(Strict.Of("abc"), Strict.Of("aBc")).Returns(false);
                yield return new TestCaseData("abc", Strict.Of("abc")).Returns(false);
                yield return new TestCaseData(Strict.Of("abc"), "abc").Returns(false);
            }
        }

        [TestCaseSource(nameof(EqualsSource))]
        public bool TestEquals(object l, object r)
        {
            return l.Equals(r);
        }


        private static IEnumerable<TestCaseData> EqualityOperatorSource
        {
            get
            {
                yield return new TestCaseData(Strict.Of("abc"), Strict.Of("abc")).Returns(true);
                yield return new TestCaseData(Strict.Of("abc"), Strict.Of("aBc")).Returns(false);
                yield return new TestCaseData(Strict.Of(new List<string>()), Strict.Of(new List<string>())).Returns(false);
            }
        }

        [TestCaseSource(nameof(EqualityOperatorSource))]
        public bool TestEqualityOperator<T>(Strict<T> l, Strict<T> r) where T: class
        {
            return l == r;
        }

    }
}
