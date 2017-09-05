using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.StringCalculatorKata;
using System.Collections.Generic;

namespace Yoox.StringCalculatorKataTest
{
    [TestClass]
    public class StringCalculatorTest_One
    {
        private StringCalculator calculator;

        public StringCalculatorTest_One()
        {
            calculator = new StringCalculator();
        }

        [TestMethod]
        public void ShouldPrintAdd()
        {
            var stringOfNumbers = new List<string>() { "", "1", "1,2", "10,11" };

            Assert.AreEqual(0, calculator.Add(stringOfNumbers[0]));
            Assert.AreEqual(1, calculator.Add(stringOfNumbers[1]));
            Assert.AreEqual(3, calculator.Add(stringOfNumbers[2]));
            Assert.AreEqual(21, calculator.Add(stringOfNumbers[3]));
        }
    }
}
