using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.StringCalculatorKata
{
    public class StringCalculator_Belli
    {
        public int Add(string numbers)
        {
            if (String.IsNullOrEmpty(numbers)) return 0;

            if (numbers.StartsWith("//"))
                return numbers.Substring(4).Split(numbers[2]).Sum(s => Int32.Parse(s));
            
            return numbers.Split(',','\n').Sum(s => Int32.Parse(s)); 
        }
    }
}
