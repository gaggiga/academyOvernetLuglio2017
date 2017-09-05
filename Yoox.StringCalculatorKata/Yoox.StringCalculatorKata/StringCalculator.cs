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
            var result = numbers.Split(',','\n').Sum(s => Int32.Parse(s));
            return result;
        }
    }
}
