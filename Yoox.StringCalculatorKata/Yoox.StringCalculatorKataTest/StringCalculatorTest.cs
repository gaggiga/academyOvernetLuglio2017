using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.StringCalculatorKata;

namespace Yoox.StringCalculatorKataTest
{
    [TestClass]
    public class StringCalculatorTest
    {
        StringCalculator sck;

        public StringCalculatorTest()
        {
            sck = new StringCalculator();
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

            foreach(var i in input)
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
    }
}
