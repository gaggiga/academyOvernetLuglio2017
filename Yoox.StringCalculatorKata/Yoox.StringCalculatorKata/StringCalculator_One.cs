using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKata
{
    public class StringCalculator_One
    {
        public int Add(string numbers)
        {
            if (numbers.Equals(""))
            {
                return 0;
            }

            return numbers.Split(',')
                          .Select(n => Int32.Parse(n))
                          .Sum();
        }
    }
}
