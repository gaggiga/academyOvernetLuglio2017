using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.StringCalculatorKata;
using Yoox.StringCalculatorKataTest;
using Yoox.StringCalculatorKataOne;

namespace Yoox.StringCalculatorKataTestOne
{
    [TestClass]
    public class StringCalculatorTest_One
    {
        StringCalculator_One sck;

        public StringCalculatorTest_One()
        {
            sck = new StringCalculator_One();
        }

        [TestMethod]
        public void Add_Should_ReturnZero_When_NumbersIsEmpty()
        {
            Assert.AreEqual(0, sck.Add(""));
        }

        [TestMethod]
        public void Add_Should_ReturnTheNumber_When_NumbersContainsSingleNumber()
        {
            var input = new string[] { "0", "1", "255" };

            foreach (var i in input)
            {
                Assert.AreEqual(Int32.Parse(i), sck.Add(i));
            }
        }

        [TestMethod]
        public void Add_Should_ReturnTheSum_When_NumbersContainsTwoNumbers()
        {
            Assert.AreEqual(3, sck.Add("1,2"));
            Assert.AreEqual(157, sck.Add("145,12"));
            Assert.AreEqual(1166, sck.Add("347,819"));
        }

        [TestMethod]
        public void Add_Should_ReturnTheSum_When_NumbersContainsMoreThenTwoNumbers()
        {
            Assert.AreEqual(158, sck.Add("145,12,1"));
            Assert.AreEqual(1171, sck.Add("347,819,4,1"));
        }

        [TestMethod]
        public void Add_Should_ReturnTheSum_When_NumbersContainsMoreThanTwoNumbers()
        {
            Assert.AreEqual(158, sck.Add("145,12,1"));
            Assert.AreEqual(1171, sck.Add("347,819,4,1"));
        }

        [TestMethod]
        public void Add_Should_SupportAlsoNewlineCharAsNumbersDelimeter()
        {
            Assert.AreEqual(10, sck.Add("1,2\n3,4"));
            Assert.AreEqual(6, sck.Add("1\n2,3"));
        }

        [TestMethod]
        public void Add_Should_SupportDelimiterReplace()
        {
            Assert.AreEqual(3, sck.Add("//;\n1;2"));
            Assert.AreEqual(157, sck.Add("//-\n145-12"));
            Assert.AreEqual(157, sck.Add("//\n\n145\n12"));
            Assert.AreEqual(1206, sck.Add("//:\n347:819:3:22:15"));
        }

        [TestMethod]
        public void Add_Should_ThrowException_When_ThereAreNegativesNumbers()
        {
            var exc = AssertExtensions.Throws<ArgumentOutOfRangeException>(() => sck.Add("-1"));
            Assert.AreEqual("Negatives not allowed: -1", exc.Message);

            exc = AssertExtensions.Throws<ArgumentOutOfRangeException>(() => sck.Add("1,-4,-3"));
            Assert.AreEqual("Negatives not allowed: -4,-3", exc.Message);
        }

        [TestMethod]
        public void Add_Should_ThrowException_When_ThereAreNegativesNumbersWithDashDelimiter()
        {
            var exc = AssertExtensions.Throws<ArgumentOutOfRangeException>(() => sck.Add("//-\n1--4-3"));
            Assert.AreEqual("Negatives not allowed: -4", exc.Message);

            exc = AssertExtensions.Throws<ArgumentOutOfRangeException>(() => sck.Add("//-\n1--42-3--23"));
            Assert.AreEqual("Negatives not allowed: -42,-23", exc.Message);

            exc = AssertExtensions.Throws<ArgumentOutOfRangeException>(() => sck.Add("//-\n-1-2-3-4"));
            Assert.AreEqual("Negatives not allowed: -1", exc.Message);

            exc = AssertExtensions.Throws<ArgumentOutOfRangeException>(() => sck.Add("//-\n-1-2--42-3--23"));
            Assert.AreEqual("Negatives not allowed: -1,-42,-23", exc.Message);
        }

        [TestMethod]
        public void Add_Should_IgnoreNumbersGreaterThanThousand()
        {
            Assert.AreEqual(158, sck.Add("145,12,1234,1"));
            Assert.AreEqual(1171, sck.Add("347,2000,819,4,1"));
        }

        [TestMethod]
        public void Add_Should_SupportAnyLengthDelimiters()
        {
            Assert.AreEqual(158, sck.Add("//[***]\n145***12***1"));
            Assert.AreEqual(1171, sck.Add("//[ - ]\n347 - 819 - 4 - 1"));
            Assert.AreEqual(1170, sck.Add("//[*[]*]\n347*[]*819*[]*4"));
            //Assert.AreEqual(3, sck.Add("//[;;;]\n1;;;2"));
            //Assert.AreEqual(6, sck.Add("//[---]\n1---2---3"));
            //Assert.AreEqual(9, sck.Add("//[[**]]\n2[**]3[**]4"));
            //Assert.AreEqual(9, sck.Add("//[///[]\n]\n]\n2///[]\n]\n3///[]\n]\n4"));
        }
    }
}
