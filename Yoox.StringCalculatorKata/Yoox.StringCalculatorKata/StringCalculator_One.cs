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

            var delimiters = new List<Char>();
            char delimiter = ',';

            if(numbers.StartsWith("//"))
            {
                delimiter = numbers[2];
                numbers = numbers.Substring(4);
            }
            else
            {
                delimiters.Add('\n');
            }

            delimiters.Add(delimiter);

            return numbers.Split(delimiters.ToArray()).Sum(s => Int32.Parse(s));
        }
    }
}
