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

            var delimiters = new char[] { ',', '\n' };

            if (numbers.StartsWith("//"))
            {
                delimiters = new char[] { numbers[2] };
                numbers = numbers.Substring(4);
            }

            var result = 0;
            foreach(var number in numbers.Split(delimiters))
            {
                var n = Int32.Parse(number);

                if(n < 0)
                {
                    throw new ArgumentOutOfRangeException("Negatives not allowed: " + number, null as Exception);
                }
                
                if(n > 1000)
                {
                    continue;
                }

                result += n;
            }

            return result;
        }
    }
}
