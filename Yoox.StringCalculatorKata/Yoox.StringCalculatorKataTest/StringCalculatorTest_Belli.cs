using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.StringCalculatorKata;

namespace Yoox.StringCalculatorKataTest
{
    [TestClass]
    public class StringCalculatorTest_Belli
    {
        StringCalculator sck = new StringCalculator();


        [TestMethod]
        public void TestEmptyString()
        {
            Assert.AreEqual(0, sck.Add(""));
        }

        [TestMethod]
        public void TestZero()
        {
            Assert.AreEqual(0, sck.Add("0"));
        }

        [TestMethod]
        public void TestOne()
        {
            Assert.AreEqual(1, sck.Add("1"));
        }

        [TestMethod]
        public void TestOnePlusTwo()
        {
            Assert.AreEqual(3, sck.Add("1,2"));
        }

        [TestMethod]
        public void TestTwoRandomNumber1()
        {
            Assert.AreEqual(157, sck.Add("145,12"));
        }

        [TestMethod]
        public void TestTwoRandomNumber2()
        {
            Assert.AreEqual(1166, sck.Add("347,819"));
        }
    }
}
