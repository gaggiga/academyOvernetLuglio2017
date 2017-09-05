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

            if (numbers.Equals(String.Empty))
            {
                return 0;
            }

            else if (numbers.Contains(","))
            {
                string[] array = new string[2];

                array = numbers.Split(',');

                return Int32.Parse(array[0]) + Int32.Parse(array[1]);
            }

            return Int32.Parse(numbers);
        }
    }
}
