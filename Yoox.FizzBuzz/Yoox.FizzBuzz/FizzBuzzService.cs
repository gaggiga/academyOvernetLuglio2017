using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yoox.FizzBuzz
{
    public class FizzBuzzService
    {
        public string Print(int v)
        {
            if (DivisibilePerCinque(v) && DivisibilePerTre(v))
            {
                return "FizzBuzz";
            }
            else if (DivisibilePerCinque(v))
            {
                return "Buzz";
            }
            else if (DivisibilePerTre(v))
            {
                return "Fizz";
            }

            return v.ToString();
        }

        private static bool DivisibilePerTre(int v)
        {
            return v % 3 == 0;
        }

        private static bool DivisibilePerCinque(int v)
        {
            return v % 5 == 0;
        }
    }
}
