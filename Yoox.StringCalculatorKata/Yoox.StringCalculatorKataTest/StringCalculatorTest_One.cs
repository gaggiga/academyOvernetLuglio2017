using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.StringCalculatorKataOne;

namespace Yoox.StringCalculatorKataTestOne
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
        public void Add_Should_ReturnTheSum_When_NumbersContainsMoreThanTwoNumbers()
        {
            Assert.AreEqual(6, sck.Add("1,2,3"));
            Assert.AreEqual(310, sck.Add("145,12,54,99"));
            Assert.AreEqual(1206, sck.Add("347,819,3,22,15"));
        }

        [TestMethod]
        public void Add_Should_SupportAlsoNewlineCharAsDelimiter()
        {
            Assert.AreEqual(3, sck.Add("1\n2"));
            Assert.AreEqual(157, sck.Add("145\n12"));
            Assert.AreEqual(1206, sck.Add("347\n819,3,22\n15"));
        }

        [TestMethod]
        public void Add_Should_SupportDifferentDelimiters()
        {
            Assert.AreEqual(3, sck.Add("//;\n1;2"));
            Assert.AreEqual(157, sck.Add("//-\n145-12"));
            Assert.AreEqual(157, sck.Add("//\n\n145\n12"));
            Assert.AreEqual(1206, sck.Add("//:\n347:819:3:22:15"));
        }
    }
}
