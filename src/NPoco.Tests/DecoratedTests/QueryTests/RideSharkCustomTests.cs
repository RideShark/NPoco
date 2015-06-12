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
            Assert.AreEqual(false, result.BoolValue);
        }

        [Test]
        public void StrTrueVarchar5IsTrue()
        {
            var result = FetchDescription(TrueBoolDatum ).First();
            Assert.AreEqual(true, result.BoolValue);
        }

        [Test]
        public void StrFalseVarchar5IsFalse()
        {
            var result = FetchDescription(FalseBoolDatum ).First();
            Assert.AreEqual(false, result.BoolValue);
        }

        [Test]
        public void StringsProperlyReturn()
        {
            var result = FetchDescription(StringIs123Datum ).First();
            Assert.AreEqual("123", result.TextData);
        }

        [Test]
        public void NullStringsConvertToEmptyString()
        {
            var result = FetchDescription(NullStringDatum).First();
            Assert.AreEqual("", result.TextData);
        }

        [Test]
        public void MixedNullsConvertProperly()
        {
            var result = FetchDescription(EvenTextDataNullsDatum).First();
            Assert.AreEqual("123", result.TextData);
            Assert.AreEqual("", result.TextData2);
            Assert.AreEqual("456", result.TextData3);
            Assert.AreEqual("", result.TextData4);
            Assert.AreEqual("789", result.TextData5);
            Assert.AreEqual(false, result.SomeInteger.HasValue);
        }
    }
}
