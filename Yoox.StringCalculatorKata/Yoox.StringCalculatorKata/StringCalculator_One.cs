using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKataOne
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            char[] delimiters = new char[] { ',', '\n' };

            return numbers.Split(delimiters).Sum(s => Int32.Parse(s));
        }
    }
}
