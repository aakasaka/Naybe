using NUnit.Framework;
using Sgk.Libs.Naybe;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaybeTests
{
    [TestFixture]
    class IDictionaryExtTest
    {
        private static IEnumerable<TestCaseData> MayGetValueSource
        {
            get
            {
                var dic = new Dictionary<int, string>
                {
                    { 11, "eleven" },
                    { 10, "ten" },
                };

                yield return new TestCaseData(dic, 10).Returns(Maybe.Just("ten"));

                yield return new TestCaseData(dic, 12).Returns(Maybe.Null<string>());

            }
        }


        [TestCaseSource(nameof(MayGetValueSource))]
        public Maybe<string> MayGetValue(Dictionary<int, string> dic, int key)
        {
            return dic.MayGetValue(key);
        }

        [TestCaseSource(nameof(MayGetValueSource))]
        public Maybe<string> MayGetValueWithIDic(IDictionary<int, string> dic, int key)
        {
            return dic.MayGetValue(key);
        }

        [TestCaseSource(nameof(MayGetValueSource))]
        public Maybe<string> MayGetValueWithIReadOnlyDic(IReadOnlyDictionary<int, string> dic, int key)
        {
            return dic.MayGetValue(key);
        }
    }
}
