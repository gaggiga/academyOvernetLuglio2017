using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKataTest
{
    public static class AssertExtensions
    {
        public static T Throws<T>(Action action) where T : Exception
        {
            T result = null;

            try
            {
                action();
                Assert.Fail("Expected exception but no exception was thrown.");
            }
            catch (T e)
            {
                Assert.IsTrue(true);
                result = e;
            }
            catch (Exception e)
            {
                Assert.Fail(string.Format("Expected exception of type {0} but no exception was thrown.", typeof(T)));
            }

            return result;
        }
    }
}
