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

            return numbers.Split(',', '\n').Sum(s => Int32.Parse(s));
        }
    }
}
