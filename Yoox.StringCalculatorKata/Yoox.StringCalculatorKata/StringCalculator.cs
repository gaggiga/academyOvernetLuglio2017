using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKata
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers)) return 0;

            var delimiters = new char[] { ',', '\n' };

            if (numbers.StartsWith("//"))
            {
                delimiters = new char[] { numbers[2] };
                numbers = numbers.Substring(4);
            }

            return numbers.Split(delimiters).Sum(s => Int32.Parse(s));
        }
    }
}
