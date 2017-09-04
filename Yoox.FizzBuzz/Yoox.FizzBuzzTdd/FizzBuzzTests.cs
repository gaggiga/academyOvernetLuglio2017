using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.FizzBuzz;

namespace Yoox.FizzBuzzTdd
{
    [TestClass]
    public class FizzBuzzTests
    {
        private FizzBuzzService _fizzBuzz;

        public FizzBuzzTests()
        {
            _fizzBuzz = new FizzBuzzService();
        }

        [TestMethod]
        public void ShouldPrintNumber()
        {
            int[] numbers = new int[] { 1, 2, 4, 7, 8, 11, 13 };

            foreach(var n in numbers)
            {
                Assert.AreEqual(n.ToString(), _fizzBuzz.Print(n));
            }
        }

        [TestMethod]
        public void ShouldPrintFizz()
        {
            int[] numbers = new int[] { 3, 6, 9, 12, 18, 21, 24, 27 };

            foreach (var n in numbers)
            {
                Assert.AreEqual("Fizz", _fizzBuzz.Print(n));
            }

        }

        [TestMethod]
        public void ShouldPrintBuzz()
        {
            int[] numbers = new int[] { 5, 10, 20, 25, 35 };

            foreach (var n in numbers)
            {
                Assert.AreEqual("Buzz", _fizzBuzz.Print(n));
            }
        }

        [TestMethod]
        public void ShouldPrintFizzBuzz()
        {
            int[] numbers = new int[] { 15, 30, 45, 60 };

            foreach (var n in numbers)
            {
                Assert.AreEqual("FizzBuzz", _fizzBuzz.Print(n));
            }
        }
    }
}
