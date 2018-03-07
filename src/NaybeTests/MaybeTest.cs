using NUnit.Framework;
using Sgk.Libs.Naybe;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaybeTests
{
    [TestFixture]
    class MaybeTest
    {
        [Test]
        public void ThrowWhenRefferToNullValue()
        {
            var m = Maybe.Null<string>();

            Assert.Throws<InvalidOperationException>(() => Console.WriteLine(m.Val));
        }

        [TestCase("", ExpectedResult =false)]
        [TestCase(null, ExpectedResult =true)]
        public bool TestIsNull(string val)
        {
            var m = new Maybe<string>(val);
            return m.IsNull;
        }

        [TestCase("", ExpectedResult = true)]
        [TestCase(null, ExpectedResult = false)]
        public bool TestHasVal(string val)
        {
            var m = new Maybe<string>(val);
            return m.HasVal;
        }

        [Test]
        public void TestCastString()
        {
            var val = "abc";
            var m = (Maybe<string>)val;

            Assert.That(m.Val, Is.EqualTo("abc"));
        }

        [Test]
        public void TestCastNull()
        {
            string val = null;
            var m = (Maybe<string>)val;

            Assert.That(m.IsNull);
        }

        [Test]
        public void ExecuteActionIfPresent()
        {
            var fired = false;

            var mNull = Maybe.Null<string>();

            mNull.IfPresent(_ => fired = true);

            Assert.That(fired, Is.False);

            var mVal = Maybe.Just("");

            mVal.IfPresent(_ => fired = true);

            Assert.That(fired, Is.True);
        }


        [TestCase("abc", "efg", ExpectedResult = "abc")]
        [TestCase(null, "efg", ExpectedResult = "efg")]
        [TestCase(null, null, ExpectedResult = null)]
        public string GetAnotherValueWhenNull(string original, string or)
        {
            var m = new Maybe<string>(original);
            return m.Or(or);
        }
    }
}
