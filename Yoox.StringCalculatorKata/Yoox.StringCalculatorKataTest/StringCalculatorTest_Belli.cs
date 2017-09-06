using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yoox.StringCalculatorKata;

namespace Yoox.StringCalculatorKataTest
{
    [TestClass]
    public class StringCalculatorTest_Belli
    {
        StringCalculator_Belli sck;

        public StringCalculatorTest_Belli()
        {
            sck = new StringCalculator_Belli();
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
        public void Add_Should_ReturnTheSum_When_NumbersContainsNewLine()
        {
            Assert.AreEqual(10, sck.Add("1,2\n3,4"));
            Assert.AreEqual(6, sck.Add("1\n2,3"));
        }

        [TestMethod]
        public void Add_Should_ReturnTheSum_When_NumbersContainsDifferentTypeOfDelimeters()
        {
           
            Assert.AreEqual(3, sck.Add("//;\n1;2"));
            Assert.AreEqual(6, sck.Add("//-\n1-2-3")); 
        }

        [TestMethod]
        public void Add_Should_IgnoreNegativeNumbers()
        {
            int result = 0;
            try
            {
                result = sck.Add("-1");
                Assert.Fail("Non scatenata eccezione");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual("Negatives not allowed: -1", e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Tipo non corretto");
            }

            try
            {
                result = sck.Add("-1,-3");
                Assert.Fail("Non scatenata eccezione");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Assert.AreEqual("Negatives not allowed: -1,-3", e.Message);
            }
            catch (Exception e)
            {
                Assert.Fail("Tipo non corretto"); 
            }

        }

        [TestMethod]
        public void Add_Should_IgnoreNumbersBiggerThan1000()
        {
            
            Assert.AreEqual(2, sck.Add("2,1001"));
            Assert.AreEqual(4, sck.Add("1,1002,3,4005"));
        }

        [TestMethod]
        public void Add_Should_ReturnTheSum_When_NumbersContainsDelimitersOfDifferentLenght()
        {

            Assert.AreEqual(3, sck.Add("//[;;;]\n1;;;2"));
            Assert.AreEqual(6, sck.Add("//[---]\n1---2---3"));
            Assert.AreEqual(9, sck.Add("//[[**]]\n2[**]3[**]4"));
            Assert.AreEqual(9, sck.Add("//[///[]\n]\n]\n2///[]\n]\n3///[]\n]\n4"));
        }
    }
}
