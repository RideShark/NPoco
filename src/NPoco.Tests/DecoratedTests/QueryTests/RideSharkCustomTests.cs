using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPoco.Tests.Common;
using NUnit.Framework;

namespace NPoco.Tests.DecoratedTests.QueryTests
{
    [TestFixture]
    public class RideSharkCustomTests : RideSharkTest 
    {
        [Test]
        public void NullVarchar5IsFalse()
        {
            var result = FetchDescription(NullBoolDatum).First();
            Assert.Equals(result.BoolValue, false);
        }

        [Test]
        public void StrTrueVarchar5IsTrue()
        {
            var result = FetchDescription(TrueBoolDatum ).First();
            Assert.Equals(result.BoolValue, true);
        }

        [Test]
        public void StrFalseVarchar5IsFalse()
        {
            var result = FetchDescription(FalseBoolDatum ).First();
            Assert.Equals(result.BoolValue, false);
        }

        [Test]
        public void StringsProperlyReturn()
        {
            var result = FetchDescription(StringIs123Datum ).First();
            Assert.Equals(result.TextData , "123");
        }

        [Test]
        public void NullStringsConvertToEmptyString()
        {
            var result = FetchDescription(NullStringDatum).First();
            Assert.Equals(result.TextData, "");
        }

    }
}
